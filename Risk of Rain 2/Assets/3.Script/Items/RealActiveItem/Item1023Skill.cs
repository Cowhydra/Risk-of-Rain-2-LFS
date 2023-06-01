using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1023Skill : PrimitiveActiveItem
{
    public float radius = 5f; // �÷��̾���� ���� �Ÿ�
    public float rotationSpeed = 50f; // ȸ�� �ӵ�

    private Transform playerTransform;


    private void Start()
    {
        Init();
        Managers.Resource.Destroy(gameObject, 30f);
    }

    public override void Init()
    {
        base.Init();

        playerTransform = Player.transform;
        InitStats();
        gameObject.SetRandomPositionSphere(2, 5, 5, Player.transform);
    }

    private void Update()
    {
        RotateAroundPlayer();
    }

    private void RotateAroundPlayer()
    {
        transform.RotateAround(playerTransform.position, Vector3.up, rotationSpeed * Time.deltaTime);

        // �÷��̾���� �Ÿ��� �����ϰ� ����
        Vector3 directionToPlayer = transform.position - playerTransform.position;
        float distanceToPlayer = directionToPlayer.magnitude;


        //ȸ�� �ݰ��� ��� ���  ���� ������ ������ �ݰ����� �Ű���
        if (distanceToPlayer > radius)
        {
            Vector3 targetPosition = playerTransform.position + directionToPlayer.normalized * radius;
            targetPosition.y += 0.01f;
            transform.position = targetPosition;
        }
    }

    private void InitStats()
    {
        int itemCount = Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1019].WhenItemActive][1019].Count;
        _playerStatus.Damage = _playerStatus._survivorsData.Damage * 3 * itemCount;
        _playerStatus.AddMaxHealth(3 * itemCount);
    }

    private void OnDisable()
    {
        _playerStatus.Damage = _playerStatus._survivorsData.Damage;
        int itemCount = Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1019].WhenItemActive][1019].Count;
        _playerStatus.AddMaxHealth(-3 * itemCount);
    }

    public void SetStats(int count)
    {
        _playerStatus.Damage = _playerStatus._survivorsData.Damage * 3 * count;
        _playerStatus.AddMaxHealth(3 * count);
    }
    private IEnumerator StartFire_co()
    {



        yield return new WaitForSeconds(5f);

    }
}
