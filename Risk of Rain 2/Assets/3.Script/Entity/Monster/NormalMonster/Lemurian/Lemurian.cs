using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lemurian : Entity
{
    [SerializeField] public MonsterData _lemurianData;
    private Animator _lemurianAnimator;
    private GameObject _player;
    
    [Header("������� ���̾�")]
    public LayerMask TargetLayer;
    private Entity _targetEntity;
    private NavMeshAgent _navMeshAgent;

    public ObjectPool FireWardPool;

    [Header("Transforms")]
    [SerializeField] private Transform _lemurianMouthTransform;

    private float[] _skillCoolDownArr = new float[2];
    private bool[] _isSkillRun = new bool[2];


    //HpBar 
    private MonsterHpBar _myHpBar;
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
        _skillCoolDownArr[0] = 2f;
        _skillCoolDownArr[1] = 1f;

        for (int i = 0; i < _isSkillRun.Length; i++)
        {
            _isSkillRun[i] = false;
        }


        _myHpBar = GetComponentInChildren<MonsterHpBar>();
        _myHpBar.gameObject.SetActive(false);
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

    private void Start()
    {
        StartCoroutine(UpdateTargetPosition_co());
    }

    private void Update()
    {
        _lemurianAnimator.SetBool("IsRun", _hasTarget);
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
            _lemurianAnimator.SetTrigger("Hit");
            _myHpBar.gameObject.SetActive(true);
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

        StartCoroutine(Destroy_co());

        Debug.Log("�������� �״� ���� �����Ÿ� ����");
        _myHpBar.gameObject.SetActive(false);
    }

    private IEnumerator Destroy_co()
    {
        yield return new WaitForSeconds(5f);
        // Ǯ�� ��ȯ ��Ű��
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
        float damage = Damage * 2;
        if (Vector3.Distance(transform.position, _targetEntity.transform.position) <= 1f)
        {
            _player.GetComponent<Entity>().OnDamage(damage); // 200%
            Debug.Log("�÷��̾ ���������� Bite�� ���� ���� damage : " + damage);
        }
    }

    private IEnumerator UpdateTargetPosition_co()
    {
        while (!IsDeath)
        {
            if (_hasTarget)
            {
                Debug.Log("Ÿ���� �ֽ��ϴ�.");
                _navMeshAgent.isStopped = false;
                _navMeshAgent.SetDestination(_targetEntity.transform.position);
                if(Vector3.Distance(transform.position, _targetEntity.transform.position) <= 10f)
                {
                    if(!_isSkillRun[1])
                    {
                        UseSkill(1);
                    }
                }
                else if(Vector3.Distance(transform.position, _targetEntity.transform.position) <= 50f)
                {
                    if (!_isSkillRun[0])
                    {
                        UseSkill(0);
                    }
                }
                else
                {
                    _targetEntity = null;
                }
            }
            else
            {
                Debug.Log("Ÿ���� �����ϴ�.");
                _navMeshAgent.isStopped = true;

                Collider[] colls = Physics.OverlapSphere(transform.position, 30f, TargetLayer);

                for (int i = 0; i < colls.Length; i++)
                {
                    if (colls[i].TryGetComponent(out Entity en))
                    {
                        if (!en.IsDeath)
                        {
                            _targetEntity = en;
                            break;
                        }
                    }
                }
            }

            yield return null;
        }
    }

    private void UseSkill(int skillIndex)
    {
        StartCoroutine(UseSkill_co(skillIndex));
    }

    private IEnumerator UseSkill_co(int skillIndex)
    {
        switch (skillIndex)
        {
            case 0:
                _lemurianAnimator.SetTrigger("FireWard"); // ��ų�� �ִϸ����Ϳ� �̺�Ʈ�� ����
                Debug.Log("�� �վ���");
                _isSkillRun[skillIndex] = true;
                break;
            case 1:
                _lemurianAnimator.SetTrigger("Bite"); // ��ų�� �ִϸ����Ϳ� �̺�Ʈ�� ����
                Debug.Log("������");
                _isSkillRun[skillIndex] = true;
                break;
        }
        yield return new WaitForSeconds(_skillCoolDownArr[skillIndex]); // ��Ÿ�Ӹ�ŭ ��ٸ���
        _isSkillRun[skillIndex] = false; // ��ų ��Ÿ�� �� ������

    }
}
