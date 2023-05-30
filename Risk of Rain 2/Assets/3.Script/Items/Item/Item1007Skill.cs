using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Item1007Skill : ItemPrimitiive
{
    private float damage;
    private float movespeed = 10.0f;
    private bool isDectedEnemy;
    private GameObject myTargetEnemy;

    // GameItemImage Target;
    //���� �̻��� �ƴ�!
    private void Start()
    {
        myTargetEnemy = GameObject.FindGameObjectWithTag("Monster");
        if (myTargetEnemy == null)
        {
            myTargetEnemy = GameObject.FindGameObjectWithTag("Boss");
            if (myTargetEnemy == null)
            {
                return;
            }
        }

        damage = Player.GetComponent<PlayerStatus>().Damage
            * 3 * (Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1007].WhenItemActive][1007].Count);


        //�÷��̾� �������� ������ ���ư���
        GetComponent<Rigidbody>().velocity = Player.transform.forward*movespeed;
        Managers.Resource.Destroy(gameObject, 15f);
        Debug.Log("�̻��� �߻��ϴ� ��ƼŬ, ���̴� �ʿ�");
       
    }
    private void OnTriggerEnter(Collider other)
    {

        //�Ѿ� �����µ� �ϳ� �� ������ �������� ����
        if (!isDectedEnemy && other.CompareTag("Monster"))
        {
            //isDectedEnemy = true;
            //myTargetEnemy = other.gameObject;
            ////������ ���ٰ� ���� �߰��� ��� �׳����� �ٰ����� ���� ���ݴ���
            //Vector3 movedir = other.transform.position - gameObject.transform.position;

            //gameObject.transform.Translate(movedir.normalized * movespeed * Time.deltaTime);

            //if ((other.transform.position - gameObject.transform.position).sqrMagnitude < 1.1f)
            //{
            //    Debug.Log("���� �� IDmage ������Ʈ �����ͼ� ������");
            //    Managers.Resource.Destroy(gameObject);
            //}

        }
     
    }

}
