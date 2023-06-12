using System.Collections;
using UnityEngine;

public class Item1023Skill : PrimitiveActiveItem
{
    public float radius = 5f; // �÷��̾���� ���� �Ÿ�
    public float rotationSpeed = 50f; // ȸ�� �ӵ�

    private Transform playerTransform;


    private void Start()
    {
        Init();
        radius = Random.Range(5, 11);
        StartCoroutine(nameof(StartFire_co));
        Managers.Resource.Destroy(gameObject, 30f);
    }

    public override void Init()
    {
        base.Init();

        playerTransform = Player.transform;

    }

    private void Update()
    {
        RotateAroundPlayer();
    }
    private void OnDisable()
    {
        StopCoroutine(nameof(StartFire_co));
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
            targetPosition.y += 0.015f;
            transform.position = targetPosition;
        }
    }

    private IEnumerator StartFire_co()
    {
        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject go = Managers.Resource.Instantiate("TurretBullet");
                go.transform.position = gameObject.transform.position;
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(2f);
        }


    }
}
