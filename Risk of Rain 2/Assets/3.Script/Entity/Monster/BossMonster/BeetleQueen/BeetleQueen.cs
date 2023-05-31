using System.Collections;
using UnityEngine;

public class BeetleQueen : Entity
{
    // TODO : ���̵��� ���� MaxHealth ������Ű��
    [SerializeField] private MonsterData _beetleQueenData;

    private GameObject _player;

    public ObjectPool AcidBallPool;
    public ObjectPool AcidPoolPool;
    public ObjectPool WardPool;
    public GameObject BombRange;

    private Animator _beetleQueenAnimator;
    private AudioSource _beetleQueenAudioSource;
    private AudioClip _hitSound;

    public bool IsRun = false;

    [Header("Transforms")]
    [SerializeField] private Transform _beetleQueenMouthTransform;
    [SerializeField] private Transform _beetleQueenButtTransform;


    //private bool hasTarget
    //{
    //    get
    //    {
    //        if (targetEntity != null && !targetEntity.IsDeath)
    //        {
    //            return true;
    //        }

    //        return false;
    //    }
    //}
    private void Awake()
    {
        TryGetComponent(out _beetleQueenAnimator);
        _player = GameObject.FindGameObjectWithTag("Player");
        _beetleQueenMouthTransform = GameObject.FindGameObjectWithTag("BeetleQueenMouth").transform;
        _beetleQueenButtTransform = GameObject.FindGameObjectWithTag("BeetleQueenButt").transform;
        AcidBallPool = GameObject.Find("AcidBallPool").GetComponent<ObjectPool>();
        AcidPoolPool = GameObject.Find("AcidPoolPool").GetComponent<ObjectPool>();
        WardPool = GameObject.Find("WardPool").GetComponent<ObjectPool>();
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
        _beetleQueenAnimator.SetTrigger("Die");
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
    /// �꼺���� 6�� ��ä�÷� �߻��ϴ� ��ų
    /// </summary>
    public void AcidBileSkill()
    {
        IsRun = true;
        Quaternion rot = Quaternion.LookRotation(_player.transform.position - _beetleQueenMouthTransform.position);
        for (int i = 0; i < 6; i++)
        {
            GameObject obj = AcidBallPool.GetObject();
            obj.transform.SetPositionAndRotation(_beetleQueenMouthTransform.position, Quaternion.Euler(0, -20f + 8 * i, 0) * rot);
        }
        //yield return new WaitForSeconds(10f);
        IsRun = false;
    }

    /// <summary>
    /// �ڲǹ��Ͽ��� ��ü 3�� �лл� �߻��ϴ� ��ų
    /// </summary>
    public void WardSkill() // ü�� 50% �̸�
    {
        IsRun = true;
        StartCoroutine(CreateWard_co());
        //yield return new WaitForSeconds(18f);
        IsRun = false;
    }

    /// <summary>
    /// �÷��̾� ��ġ�� �ð��� ���� ���� �ϴ� ��ų
    /// </summary>
    public void RangeBombSkill() // ü�� 25% �̸�
    {
        IsRun = true;
        Vector3 pos = Vector3.zero;
        RaycastHit[] hits;
        Ray ray = new Ray(_player.transform.position, Vector3.down);

        hits = Physics.RaycastAll(ray);

        foreach (RaycastHit obj in hits)
        {
            if (obj.transform.gameObject.CompareTag("Ground"))
            {
                pos = obj.point;
                pos = new Vector3(pos.x, pos.y + 0.2f, pos.z);
                Instantiate(BombRange, pos, Quaternion.identity);
            }
        }
        //yield return new WaitForSeconds(20f);
        IsRun = false;
    }

    private IEnumerator CreateWard_co()
    {
        //Quaternion rot = Quaternion.LookRotation(_player.transform.position - _beetleQueenMouthTransform.position);
        WaitForSeconds wfs = new WaitForSeconds(0.3f);
        for (int i = 0; i < 3; i++)
        {
            GameObject obj = WardPool.GetObject();
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
