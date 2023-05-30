using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ �й�� -> �������� ��ȯ, ���� �� ü�� ���ʽ� �ش�.
public class Item1019Skill : ItemPrimitiive
{

    public float radius = 5f; // �ݰ�
    public float rotationSpeed = 10f; // �ӵ�

    private Vector3 rotationAxis;
    private PlayerStatus playerStatus;

    private void Start()
    {
        // �߽����� ��ü�� �ʱ� �Ÿ� �� ��(axis) ����
        rotationAxis = (transform.position - Player.transform.position).normalized;
        playerStatus=Player.GetComponent<PlayerStatus>();
        playerStatus.Damage = playerStatus._survivorsData.Damage * 3* (Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1019].WhenItemActive][1019].Count);
        playerStatus.MaxHealth = playerStatus._survivorsData.MaxHealth * 3 * (Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1019].WhenItemActive][1019].Count);
    }

    private void Update()
    {
        rotationAxis = (transform.position - Player.transform.position).normalized;


        // �߽����� �߽����� ��ü�� ȸ����Ŵ
        transform.RotateAround(Player.transform.position, rotationAxis, rotationSpeed * Time.deltaTime);
        // ȸ�� �� ��ü�� ��ġ�� �ݰ濡 �°� ����
        transform.position = Player.transform.position + (transform.position - Player.transform.position).normalized * radius;


    }
    private void OnDisable()
    {
        playerStatus.Damage = playerStatus._survivorsData.Damage ;
        playerStatus.MaxHealth = playerStatus._survivorsData.MaxHealth;
    }
}
