using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1016Component : ItemPrimitiive
{
    // Start is called before the first frame update
    private float _damage;
    private bool _isTakeDamageable = false;
    private float damageCoolTime = 1.0f;


    private void Start()
    {
        Init();
        
        gameObject.SetRandomPositionSphere(1, 1, -Player.GetComponent<Collider>().bounds.size.y-1f, Player.transform);
        StartCoroutine(nameof(JumpingStart_co));
    }

    public override void Init()
    {

        base.Init();

    }

    public void SetDamage(int Count)
    {
        _damage = _playerStatus.Damage* 5.5f * Count;
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) //������ �ױ׷� ���ϰ� ������, ������Ʈ�� ���� ��ü�� �ҷ��;� �� 
        {
            if (!_isTakeDamageable)
            {
                StopCoroutine(nameof(TakeDamage_co));
                StartCoroutine(nameof(TakeDamage_co), other);

            }


        }
    }
    private IEnumerator TakeDamage_co(Collider coll)
    {
        _isTakeDamageable = true;
        if (coll.TryGetComponent(out Entity entity))
        {
            entity.OnDamage(_damage);
        }
        else
        {
            Debug.Log($"{coll.gameObject.name}�� Entity�� ã�� ����");
        }

        yield return new WaitForSeconds(damageCoolTime);
        _isTakeDamageable = false;
    }

    private IEnumerator JumpingStart_co()
    {
        yield return new WaitForSeconds(1.2f);
        Managers.Resource.Destroy(gameObject);

    }
}
