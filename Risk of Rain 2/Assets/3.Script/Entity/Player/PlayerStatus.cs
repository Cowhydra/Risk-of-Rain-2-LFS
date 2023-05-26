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
    [HideInInspector] public string Name;
    [HideInInspector] public float Mass;
    [HideInInspector] public float CriticalChance;
    [HideInInspector] public int MaxJumpCount;
    //Survivors Data�� ������� ���� ����
    [HideInInspector] public int Level;
    [HideInInspector] public float Exp;
    [HideInInspector] public float Gold;

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
}
