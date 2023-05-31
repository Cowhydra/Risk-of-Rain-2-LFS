using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
/// <summary>
/// �÷��̾� �������ͽ� Ŭ����, �⺻ ������ Entity�� �ְ� �÷��̾ ������ ������ ���⼭ ����
/// </summary>
public class PlayerStatus : Entity
{
    public  SurvivorsData _survivorsData;
    //Survivors Data���� ������ ����
    public string Name { get; private set; }
    public float Mass { get; private set; }
    public float CriticalChance { get;  set; }
    public int MaxJumpCount { get; private set; }

    //Survivors Data�� ������� ���� ����
    public int Level { get; private set; }
    public float Exp { get; private set; } = 100f;
    public float CurrentExp { get; private set; }
    public float ChanceBlockDamage { get;  set; }

    protected override void OnEnable()
    {
        InitStatus();
        base.OnEnable();
        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerHpChange, this);
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
    //���� �ִ� �ͺ��� OnHeal, OnDamage�� �ִ°� ���� �� ���Ƽ� ����!
    public void OnHeal(float heal)
    {
        Health += heal;
        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerHpChange, this);
    }
    public override void OnDamage(float damage)
    {
        if(GetBlockChanceResult())
        {
            base.OnDamage(damage);
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerHpChange, this);
        }
    }

    public bool GetBlockChanceResult()
    {
        bool result = false;
        if(Random.Range(1,101) <= ChanceBlockDamage)
        {
            //ChanceBlockDamage ���� ���� �������ָ� �ɵ�! -KYS
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
            Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerExpChange, this);
        }
    }
    public void IncreaseExp(float exp)
    {
        CurrentExp += exp;
        Managers.Event.PostNotification(Define.EVENT_TYPE.PlayerExpChange, this);
    }
}