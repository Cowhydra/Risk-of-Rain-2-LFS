using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//�ǽĿ� �ܰ� 3���� ���� �ܰ� �߻�
public class item1014Skill : ItemPrimitiive
{
  //  List<GameObject> Enemys;
    private float damage;
    float movespeed = 5.0f;
    private bool isDectedEnemy;
    private GameObject myTargetEnemy;
    private void Start()
    {
     //   Enemys = GameObject.FindGameObjectsWithTag("Monster").ToList();
      //  Enemys.Add(GameObject.FindGameObjectWithTag("Boss"));
        damage = Player.GetComponent<PlayerStatus>().Damage
            * 1.5f*(Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1014].WhenItemActive][1014].Count);

    }


    //��� 1. ��� ���� �ܾ� ��Ƽ� ���� ����� ������ ������ ���ϴ� ��� ( �ݶ��̴��� ���� �۰� �ؼ� Triggeró��)
    // �߻������� ���� 1. �� ���� , Ÿ�� ���� -> Ÿ�� ���� Die ���� �� ü�� Ȯ���Ͽ� �Ѱܾ��� 
    // �ٵ� ã�Ƽ� �Ѱ�µ� �׾��� �� ���� 
    //��� 2. �ݶ��̴��� �ſ� ũ�� ���� Stay �� �ִ� �� ��� ã�� .. �׸��� �Ÿ��� ���� �Ÿ� 0.5f? ������ ��������� ������Ŵ
    // ��ų ������ �νĹ����� �дٰ� ������  �켱 2������� ����
    //private void SetTarget()
    //{
    //    foreach(GameObject enemy in Enemys)
    //    {
    //        if(enemy.H)
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {

        //other�� �ױ׸� �������� �κ��� �ϴ� ���ΰ� �÷��̾ ����
        //IDamge�� ���� �����ϸ� �ױ� ��ߴ�� ���ϸ� �±� ���� �Ƚᵵ �ɵ�?
        if (!isDectedEnemy&&other.CompareTag("Monster"))
        {
            isDectedEnemy = true;
            myTargetEnemy = other.gameObject;
            Vector3 movedir = other.transform.position - gameObject.transform.position;

            gameObject.transform.Translate(movedir.normalized * movespeed * Time.deltaTime);

            if ((other.transform.position-gameObject.transform.position).sqrMagnitude < 1.1f)
            {
                Debug.Log("���� �� IDmage ������Ʈ �����ͼ� ������");
                Managers.Resource.Destroy(gameObject);
            }

        }
       

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
