using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1003SkillComponent : ItemPrimitiive
{
    private float movespeed = 5f;

    [SerializeField] private float SkillDectDistance = 8f;
    private void Awake()
    {
        base.Init();

    }

    private void OnEnable()
    {
        StartCoroutine(nameof(ItemSpawnTime_co));
    }
    private IEnumerator ItemSpawnTime_co()
    {
        yield return new WaitForSeconds(7.0f);
        Managers.Resource.Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            //���� �Ÿ� �ȿ� ���� ��쿡�� ü�� ���� ���󰡵��� ==> �ݶ��̴��� ��Ӻ������� ũ�� ���
            // ���� �ȿ� ������ �� �Ÿ��� �����Ͽ� ����� ���� -> ������ �ٰ�����  ���� ->
            // �ָ� ������ �ʰ� �׳� ������ �ƹ��͵� ����
            //�Ÿ� Ȯ��
            if ((other.transform.position - gameObject.transform.position).magnitude < SkillDectDistance)
            {
                //�̵�
                Vector3 movedir = Player.transform.position - gameObject.transform.position;
                gameObject.transform.Translate(movedir.normalized * movespeed * Time.deltaTime);

                if ((other.transform.position - gameObject.transform.position).magnitude < SkillDectDistance * 0.3f)
                {
                    //�� ����
                    other.GetComponent<PlayerStatus>().OnHeal(8 +
                   other.GetComponent<PlayerStatus>().MaxHealth
                          * 0.02f * Managers.ItemInventory.Items[1003].Count);
                    Managers.Resource.Destroy(gameObject);
                    Debug.Log("ȸ�� ŰƮ ������ ��Ÿ�� ����Ʈ");
                }

            }
            else
            {
                //�Ÿ��� �� ���

            }

        }
    }
}
