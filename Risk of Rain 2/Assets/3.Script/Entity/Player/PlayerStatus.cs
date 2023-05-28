using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �÷��̾� �������ͽ� Ŭ����, �⺻ ������ Entity�� �ְ� �÷��̾ ������ ������ ���⼭ ����
/// </summary>
public class PlayerStatus : Entity
{
    [SerializeField] private SurvivorsData _survivorsData;
    //Survivors DAta���� ������ ����
    public string Name { get; private set; }
    public float Mass { get; private set; }
    public float CriticalChance { get; private set; }
    public int MaxJumpCount { get; private set; }

    //Survivors Data�� ������� ���� ����
    public int Level { get; private set; }
    public float Exp { get; private set; }
    public float Gold { get; private set; }
    public float ChanceBlockDamage { get; private set; }

    protected override void OnEnable()
    {
        InitStatus();
        base.OnEnable();
    }

    private void InitStatus()
    {
        Name = _survivorsData.Name;
        MaxHealth = _survivorsData.MaxHealth;
        Damage = _survivorsData.Damage;
        HealthRegen = _survivorsData.HealthRegen;
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
        if(Random.Range(0,100) <= ChanceBlockDamage)
        {
            result = true;
        }
        return result;
    }
}
