using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1008SkillComponent : ItemPrimitiive
{
    private float damageCoolTime = 1.5f;
    private float damage;
    private bool IsExcute = false;

    //�� ���� ��ġ�� �����ؾ� �ϴµ� �� ��ġ�� ��� �޾ƿ� ���ΰ�??
    //����ȭ�� �ؾ��� ���ΰ�?.. �����Ұ� ������

    public override void Init()
    {
        base.Init();
        damage = _playerStatus.Damage
* 3.5f * Managers.ItemInventory.Items[1008].Count;
        Managers.Resource.Destroy(gameObject, 4.0f);
    }

    private void Start()
    {
        Init();
    }
    //���ǵ� 
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) //������ �ױ׷� ���ϰ� ������, ������Ʈ�� ���� ��ü�� �ҷ��;� �� 
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
        if (coll.TryGetComponent(out Entity entity))
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
