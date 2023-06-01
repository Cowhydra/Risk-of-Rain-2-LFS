using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1014SkillComponent : ItemPrimitiive
{
    //  List<GameObject> Enemys;
    private float damage;
    float movespeed = 8.0f;
    private bool isDectedEnemy = false;
    private GameObject myTargetEnemy;
    private float rotateSpeed = 800.0f;

    private Entity enemyEntity;

    public override void Init()
    {
        base.Init();
        damage = Player.GetComponent<PlayerStatus>().Damage
          * 1.5f * Managers.ItemInventory.Items[1014].Count;

    }
    private void Start()
    {
        //   Enemys = GameObject.FindGameObjectsWithTag("Monster").ToList();
        //  Enemys.Add(GameObject.FindGameObjectWithTag("Boss"));
        Init();
        GetComponent<Rigidbody>().velocity = Player.transform.forward * movespeed;
        Managers.Resource.Destroy(gameObject, 10f);
    }


    //��� 1. ��� ���� �ܾ� ��Ƽ� ���� ����� ������ ������ ���ϴ� ��� ( �ݶ��̴��� ���� �۰� �ؼ� Triggeró��)
    // �߻������� ���� 1. �� ���� , Ÿ�� ���� -> Ÿ�� ���� Die ���� �� ü�� Ȯ���Ͽ� �Ѱܾ��� 
    // �ٵ� ã�Ƽ� �Ѱ�µ� �׾��� �� ���� 
    //��� 2. �ݶ��̴��� �ſ� ũ�� ���� Stay �� �ִ� �� ��� ã�� .. �׸��� �Ÿ��� ���� �Ÿ� 0.5f? ������ ��������� ������Ŵ
    // ��ų ������ �νĹ����� �дٰ� ������  �켱 2������� ����

    private void OnTriggerStay(Collider other)
    {

        //other�� �ױ׸� �������� �κ��� �ϴ� ���ΰ� �÷��̾ ����
        //IDamge�� ���� �����ϸ� �ױ� ��ߴ�� ���ϸ� �±� ���� �Ƚᵵ �ɵ�?
        if (!other.CompareTag("Player"))
        {
            if (!isDectedEnemy && other.TryGetComponent(out Entity entity))
            {


                isDectedEnemy = true;
                myTargetEnemy = other.gameObject;
                enemyEntity = entity;

                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().velocity = movespeed * (myTargetEnemy.transform.position - gameObject.transform.position).normalized;


            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isDectedEnemy && collision.gameObject.Equals(myTargetEnemy))
        {
            if ((myTargetEnemy.transform.position - gameObject.transform.position).sqrMagnitude < 5f)
            {
                Debug.Log("Ž����!");
                enemyEntity.OnDamage(damage);
                Managers.Resource.Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        transform.RotateAround(transform.position, Vector3.forward, rotateSpeed * Time.deltaTime);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == myTargetEnemy)
        {
            isDectedEnemy = false;
            myTargetEnemy = null;
        }
    }

}
