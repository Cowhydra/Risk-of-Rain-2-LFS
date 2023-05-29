using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1003Skill : ItemPrimitiive
{
    private float movespeed = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            other.GetComponent<PlayerStatus>().Health = 8+ 
                other.GetComponent<PlayerStatus>().MaxHealth
                * 0.02f * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1003].WhenItemActive][1003].Count;
            Managers.Resource.Destroy(gameObject);

            Debug.Log("ȸ�� ŰƮ ������ ��Ÿ�� ����Ʈ");
        }
    }
    private void FixedUpdate()
    {
        Vector3 movedir = Player.transform.position - gameObject.transform.position;

        gameObject.transform.Translate(movedir.normalized * movespeed * Time.deltaTime);
    }


}
