using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lemurian : Entity
{
    [SerializeField] private MonsterData _lemurianData;
    private GameObject _player;

    public ObjectPool FireWardPool;

    public Animator LemurianAnimator;

    [Header("Transforms")]
    [SerializeField] private Transform _lemurianMouthTransform;

    private void Awake()
    {
        TryGetComponent(out LemurianAnimator);
        _player = GameObject.FindGameObjectWithTag("Player");
        _lemurianMouthTransform = GameObject.FindGameObjectWithTag("LemurianMouth").transform;
        FireWardPool = GameObject.Find("FireWardPool").GetComponent<ObjectPool>();
    }

    protected override void OnEnable()
    {
        SetUp(_lemurianData);
        base.OnEnable();
        Debug.Log("Health : " + Health);
        Debug.Log("IsDeath : " + IsDeath);
        Debug.Log("Damage : " + Damage);
        Debug.Log("MoveSpeed : " + MoveSpeed);
        Debug.Log("Armor : " + Armor);
        Debug.Log("MaxHealthAscent : " + MaxHealthAscent);
        Debug.Log("DamageAscent : " + DamageAscent);
        Debug.Log("HealthRegen : " + HealthRegen);
        Debug.Log("HealthRegenAscent : " + HealthRegenAscent);
    }

    private void SetUp(MonsterData data)
    {
        MaxHealth = data.MaxHealth;
        Damage = data.Damage;
        MoveSpeed = data.MoveSpeed;
        Armor = data.Amor;
        MaxHealthAscent = data.MaxHealthAscent;
        DamageAscent = data.DamageAscent;
        HealthRegen = data.HealthRegen;
        HealthRegenAscent = data.RegenAscent;
    }

    public override void OnDamage(float damage)
    {
        if (!IsDeath)
        {
            //.Play();
            //.PlayOneShot(hitSound);
        }

        base.OnDamage(damage);
    }

    public override void Die()
    {
        base.Die();
        LemurianAnimator.SetTrigger("Die");
    }

    /// <summary>
    /// ���Ÿ������� �÷��̾�� �Ÿ��� �ΰ� �������� �̵��ϸ鼭 100%�� ���ظ� ���� �ҵ��̸� �߻�
    /// </summary>
    public void FireWardSkill()
    {
        Quaternion rot = Quaternion.LookRotation(_player.transform.position - _lemurianMouthTransform.position);
        GameObject obj = FireWardPool.GetObject();
        obj.transform.SetPositionAndRotation(_lemurianMouthTransform.position, Quaternion.Euler(0, 0, 0) * rot);
    }

    /// <summary>
    /// 10m �̳��� �����ϸ� �޷��ͼ� ������(bite) ������ �� 200%�� ���ظ� ����. ������ ������ 1���� ��Ÿ���� ����
    /// </summary>
    public void BiteSkill() // ����Ʈ�� �ִ��� ������ �𸣰���
    {
        // �ִϸ����� ���� ��ũ��Ʈ�θ� ���� �Ѱ� ������?
    }
}
