using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
/// <summary>
/// �÷��̾� �������ͽ� Ŭ����, �⺻ ������ Entity�� �ְ� �÷��̾ ������ ������ ���⼭ ����
/// </summary>
public class PlayerStatus : Entity
{
<<<<<<< HEAD
    [HideInInspector]
    public string Name;
    [HideInInspector]
    public float Mass;
    [HideInInspector]
    public float CriticalChance;
    [HideInInspector]
    public int MaxJumpCount;
    public  SurvivorsData _survivorsData;
=======
    [SerializeField] private SurvivorsData _survivorsData;
    //Survivors Data���� ������ ����
    public string Name { get; private set; }
    public float Mass { get; private set; }
    public float CriticalChance { get; private set; }
    public int MaxJumpCount { get; private set; }

    //Survivors Data�� ������� ���� ����
    public int Level { get; private set; }
    public float Exp { get; private set; } = 100f;
    public float CurrentExp { get; private set; }
    public float ChanceBlockDamage { get; private set; }
>>>>>>> feature/Player

    protected override void OnEnable()
    {
        InitStatus();
        base.OnEnable();
    }

    private void Update()
    {
        CheckLevel();
    }

    private void InitStatus()
    {
        Name = _survivorsData.Name;
        MaxHealth = _survivorsData.MaxHealth;
        Damage = _survivorsData.Damage;
        DamageAscent = _survivorsData.DamageAscent;
        HealthRegen = _survivorsData.HealthRegen;
        HealthRegenAscent = _survivorsData.HealthRegenAscent;
        Armor = _survivorsData.Armor;
        MoveSpeed = _survivorsData.MoveSpeed;
        Mass = _survivorsData.Mass;
        CriticalChance = _survivorsData.CriticalChance;
        MaxJumpCount = _survivorsData.MaxJumpCount;
    }

    public override void OnDamage(float damage)
    {
        if(GetBlockChanceResult())
        {
            base.OnDamage(damage);
        }
    }

    private bool GetBlockChanceResult()
    {
        bool result = false;
        if(Random.Range(1,101) <= ChanceBlockDamage)
        {
            result = true;
        }
        return result;
    }

    private void CheckLevel()
    {
        if(CurrentExp >= Exp)
        {
            Level++;
            CurrentExp = 0f;
            Exp *= 1.55f;
        }
    }
    public void IncreaseExp(float exp)
    {
        CurrentExp += exp;
    }
}