using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ �й�� -> �������� ��ȯ, ���� �� ü�� ���ʽ� �ش�.
public class Item1019Skill : ItemPrimitiive
{

    public float radius = 5f; // �ݰ�
    public float rotationSpeed = 50f; // �ӵ�

    private Vector3 rotationAxis;

    public override void Init()
    {
        base.Init();

        // �߽����� ��ü�� �ʱ� �Ÿ� �� ��(axis) ����
        rotationAxis = (transform.position - Player.transform.position).normalized;
        _playerStatus.Damage = _playerStatus._survivorsData.Damage * 3 * (Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1019].WhenItemActive][1019].Count);
        _playerStatus.AddMaxHealth(3 * (Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1019].WhenItemActive][1019].Count));
    }
    private void Start()
    {
        Init();

        Debug.Log("������ ��Ÿ���� ������ Courtine");
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
        _playerStatus.Damage = _playerStatus._survivorsData.Damage ;
        _playerStatus.AddMaxHealth( -3 * (Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1019].WhenItemActive][1019].Count));
    }
    public void SetStats(int Count)
    {
        _playerStatus.Damage = _playerStatus._survivorsData.Damage * 3 * Count;
        _playerStatus.AddMaxHealth(3 * Count);

    }
}
