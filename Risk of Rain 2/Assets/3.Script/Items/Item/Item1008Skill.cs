using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

//���ƺ�� -> ��ϻ���
public class Item1008Skill : ItemPrimitiive
{
    private float damageCoolTime = 1.0f;
    private float damage;
    private bool IsExcute = false;

    //�� ���� ��ġ�� �����ؾ� �ϴµ� �� ��ġ�� ��� �޾ƿ� ���ΰ�??
    //����ȭ�� �ؾ��� ���ΰ�?.. �����Ұ� ������

    private void Start()
    {
        damage = Player.GetComponent<PlayerStatus>().Damage
    * 3.5f * (Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1008].WhenItemActive][1008].Count);
    }
    //���ǵ� 
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Define.BossTag)) //������ �ױ׷� ���ϰ� ������, ������Ʈ�� ���� ��ü�� �ҷ��;� �� 
        {
            if (!IsExcute)
            {
                StopCoroutine(nameof(TakeDamage_co));
                StartCoroutine(nameof(TakeDamage_co), other);

            }
   

        }
    }


    private IEnumerator TakeDamage_co(Collider coll)
    {
        IsExcute = true;
        if(coll.TryGetComponent(out Entity entity))
        {
           entity.OnDamage(damageCoolTime);
        }
        else
        {
            Debug.Log($"{coll.gameObject.name}�� Entity�� ã�� ����");
        }
      
        yield return new WaitForSeconds(damageCoolTime);
        IsExcute = false;
    }
}
