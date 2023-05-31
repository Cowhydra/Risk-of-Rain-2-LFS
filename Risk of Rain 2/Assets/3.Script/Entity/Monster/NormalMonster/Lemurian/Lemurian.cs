using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lemurian : Entity
{
    [SerializeField] public MonsterData _lemurianData;
    private Animator _lemurianAnimator;
    public GameObject _player;
    
    [Header("������� ���̾�")]
    public LayerMask TargetLayer;

    private Entity _targetEntity;
    private NavMeshAgent _navMeshAgent;

    public ObjectPool FireWardPool;

    [Header("Transforms")]
    [SerializeField] private Transform _lemurianMouthTransform;

    private bool _hasTarget
    {
        get
        {
            if (_targetEntity != null && !_targetEntity.IsDeath)
            {
                return true;
            }

            return false;
        }
    }
    private void Awake()
    {
        TryGetComponent(out _navMeshAgent);
        TryGetComponent(out _lemurianAnimator);
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
    private void Update()
    {
        //_lemurianAnimator.SetBool("IsRun", _hasTarget);
        _navMeshAgent.SetDestination(_player.transform.position);
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
        _navMeshAgent.speed = data.MoveSpeed;
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
        _lemurianAnimator.SetTrigger("Die");

        Collider[] colls = GetComponents<Collider>();
        foreach (Collider col in colls)
        {
            col.enabled = false;
        }

        _navMeshAgent.isStopped = true;
        _navMeshAgent.enabled = false;

        Debug.Log("�������� �״� ���� �����Ÿ� ����");
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
        OnDamage(Damage * 2); // 200%
    }
}
