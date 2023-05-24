using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleQueen : Entity
{
    // TODO : ���̵��� ���� MaxHealth ������Ű��
    [SerializeField] private MonsterData _beetleQueenData;

    private Entity targetEntity;

    public ObjectPool AcidBallPool;
    public ObjectPool AcidPoolPool;
    public ObjectPool BombPool;

    public Animator BeetleQueenAnimator;
    private AudioSource _beetleQueenAudioSource;
    private AudioClip _hitSound;

    [Header("Transforms")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _beetleQueenMouthTransform;
    [SerializeField] private Transform _beetleQueenButtTransform;


    private bool hasTarget
    {
        get
        {
            if (targetEntity != null && !targetEntity.IsDeath)
            {
                return true;
            }

            return false;
        }
    }
    private void Awake()
    {
        TryGetComponent(out BeetleQueenAnimator);
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _beetleQueenMouthTransform = GameObject.FindGameObjectWithTag("BeetleQueenMouth").transform;
        _beetleQueenButtTransform = GameObject.FindGameObjectWithTag("BeetleQueenButt").transform;
        AcidBallPool = GameObject.FindGameObjectWithTag("AcidBallPool").GetComponent<ObjectPool>();
        AcidPoolPool = GameObject.FindGameObjectWithTag("AcidPoolPool").GetComponent<ObjectPool>();
        BombPool = GameObject.FindGameObjectWithTag("BombPool").GetComponent<ObjectPool>();
    }
    
    protected override void OnEnable()
    {
        SetUp(_beetleQueenData);
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

    public override void OnDamage(float damage)
    {
        if (!IsDeath)
        {
            //hitEffect.transform.SetPositionAndRotation(hitposition, Quaternion.LookRotation(hitnormal)); / ��?��
            //hitEffect.Play();
            //_beetleQueenAudio.PlayOneShot(hitSound);
        }

        base.OnDamage(damage);
    }

    public override void Die()
    {
        base.Die();
        BeetleQueenAnimator.SetTrigger("Die");
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

    /// <summary>
    /// 
    /// 
    /// </summary>
    public void StartAcidBileSkill()
    {
        Quaternion rot = Quaternion.LookRotation(_playerTransform.position - _beetleQueenMouthTransform.position);
        for (int i = 0; i < 6; i++)
        {
            GameObject obj = AcidBallPool.GetObject();
            obj.transform.SetPositionAndRotation(_beetleQueenMouthTransform.position, Quaternion.Euler(0, -20f + 8 * i, 0) * rot);
        }
    }

    /// <summary>
    /// 
    /// 
    /// </summary>
    public void StartBombSkill()
    {
        StartCoroutine(CreateBomb_co());
    }

    private IEnumerator CreateBomb_co()
    {
        Quaternion rot = Quaternion.LookRotation(_playerTransform.position - _beetleQueenMouthTransform.position);
        WaitForSeconds wfs = new WaitForSeconds (0.3f);
        for(int i = 0; i < 3; i++)
        {
            GameObject obj = BombPool.GetObject();
            obj.transform.position = _beetleQueenButtTransform.position;
            yield return wfs;
        }
    }

    /// <summary>
    /// �ִϸ��̼� ������ ȸ����ų�� ����ϴ� �޼ҵ�
    /// </summary>
    /// <param name="angle"></param>
    public void Rotate(float angle)
    {
        transform.Rotate(new Vector3(0, angle, 0));
    }
}