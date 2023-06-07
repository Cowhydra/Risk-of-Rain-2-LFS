using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1015SkillComponent : ItemPrimitiive
{
    private float damageCoolTime = 1.0f;
    private float damage;
    private bool IsExcute = false;
    private float prevMoveSpeed;
    private Vector3 prevTransformScale;

    private float _remainTime=10.0f;
    private float _realTime = 0f;
    public override void Init()
    {
        base.Init();


    }
    public void ReCall(int Count)
    {
        _realTime = 0.0f;
        gameObject.transform.localScale = new Vector3(
        prevTransformScale.x * (1 + 0.1f * Count),
        prevTransformScale.y,
        prevTransformScale.z * (1 + 0.1f * Count));

    }
    private void Awake()
    {
        Init();
    }
    private void OnDisable()
    {
        StopCoroutine(nameof(RemainTime_co));
    }
    private void OnEnable()
    {
        StartCoroutine(nameof(RemainTime_co));
        _realTime = 0f;
        damage = _playerStatus.Damage
* 3.5f * (Managers.ItemInventory.Items[1015].Count);
        prevTransformScale = gameObject.transform.localScale;

    }
    private IEnumerator RemainTime_co()
    {
        while (true)
        {
            _realTime += Time.deltaTime;

            if (_realTime > _remainTime)
            {
                Managers.Resource.Destroy(gameObject);
            }
            yield return null;
        }

    }



    //���ǵ� 
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) //������ �ױ׷� ���ϰ� ������, ������Ʈ�� ���� ��ü�� �ҷ��;� �� 
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
        if (!other.CompareTag("Player")) //������ �ױ׷� ���ϰ� ������, ������Ʈ�� ���� ��ü�� �ҷ��;� �� 
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
        ShowDamageUI(coll.gameObject, damage, Define.EDamageType.Item);
        yield return new WaitForSeconds(damageCoolTime);
        IsExcute = false;
    }

}
