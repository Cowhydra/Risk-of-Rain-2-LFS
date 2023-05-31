using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��������
public class Item1015Skill : ItemPrimitiive
{
    private float damageCoolTime = 1.0f;
    private float damage;
    private bool IsExcute = false;
    private float prevMoveSpeed;
    private Vector3 prevTransformScale;
    //�� ���� ��ġ�� �����ؾ� �ϴµ� �� ��ġ�� ��� �޾ƿ� ���ΰ�??
    //����ȭ�� �ؾ��� ���ΰ�?.. �����Ұ� ������

    private void Start()
    {
        damage = Player.GetComponent<PlayerStatus>().Damage
    * 3.5f * (Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1015].WhenItemActive][1015].Count);
        prevTransformScale=gameObject.transform.localScale;

        gameObject.transform.localScale = new Vector3(
            prevTransformScale.x * (1 + 0.1f * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1015].WhenItemActive][1015].Count),
            prevTransformScale.y,
            prevTransformScale.z * (1 + 0.1f * Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1015].WhenItemActive][1015].Count));

    }
    //���ǵ� 
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Monster")||other.CompareTag(Define.BossTag)) //������ �ױ׷� ���ϰ� ������, ������Ʈ�� ���� ��ü�� �ҷ��;� �� 
        {
            //�̼��� ��� - �������� 1�ʸ��� 

            if (other.TryGetComponent(out Entity MonsterEntity))
            {
                prevMoveSpeed = MonsterEntity.MoveSpeed;
                MonsterEntity.MoveSpeed = prevMoveSpeed * 0.8f;
                if (!IsExcute)
                {
                    StopCoroutine(nameof(TakeDamage_co));
                    StartCoroutine(nameof(TakeDamage_co), other);

                }
            }
            else
            {
                Debug.Log("Entiotly �����������"); 
            }
           


        }
    }
    private void Update()
    {
        gameObject.transform.position = Player.transform.position;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster") || other.CompareTag(Define.BossTag)) //������ �ױ׷� ���ϰ� ������, ������Ʈ�� ���� ��ü�� �ҷ��;� �� 
        {
            if (other.TryGetComponent(out Entity entity))
            {
                entity.MoveSpeed = prevMoveSpeed;
            }
            else
            {
                Debug.Log($"{other.gameObject.name}�� Entity�� ã�� �� ����");
            }
        }
    }

    private IEnumerator TakeDamage_co(Collider coll)
    {
        IsExcute = true;
        coll.GetComponent<Entity>().OnDamage(damage);
        yield return new WaitForSeconds(damageCoolTime);
        IsExcute = false;
    }
}
