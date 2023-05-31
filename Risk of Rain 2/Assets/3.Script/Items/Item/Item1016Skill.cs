using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1016Skill : ItemPrimitiive
{
    // Start is called before the first frame update
    private float damage;
    private bool IsExcute = false;
    private float damageCoolTime = 1.0f;
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        damage = Player.GetComponent<PlayerStatus>().Damage
* 5.5f * (Managers.ItemInventory.WhenActivePassiveItem[Managers.ItemInventory.PassiveItem[1016].WhenItemActive][1016].Count);
        Managers.Resource.Destroy(gameObject,2.0f);
    }

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
