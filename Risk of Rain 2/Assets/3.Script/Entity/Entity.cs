using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �÷��̾�� ���Ͱ� ������ Ŭ����,
/// ���� : MaxHealth, Damage, MoveSpeed, Armor, MaxHealthAscent, DamageAscent, HealthRegen, HealthRegenAscent
/// </summary>
public class Entity : MonoBehaviour
{
    // �ִ�ü�� o
    // ü�� o
    // ���ݷ� Damage
    // �ӵ� Speed
    // ���� Defense
    // ������ ü�� ���ġ MaxHealthAscent
    // ������ ���ݷ� ���ġ DamageAscent

    // ü�� ȸ��
    // ������ ü�� ȸ�� ���ġ

    // MonsterData //
    // MaxHealth
    // Damage
    // Speed
    // Defense
    // MaxHealthAscent
    // DamageAscent

    // --------------------------------------
    [HideInInspector]
    public float MaxHealth; // ������ ���� �þ
    public float Health { get; protected set; }
    public bool IsDeath { get; protected set; }
    public event Action OnDeath;

    public float Damage { get; protected set; } // ���ݷ�
    public float MoveSpeed { get; protected set; } // �ӵ�
    public float Armor { get; protected set; } // ����
    public float MaxHealthAscent { get; protected set; } // ������ ü�� ���ġ
    public float DamageAscent { get; protected set; } // ������ ���ݷ� ���ġ
    public float HealthRegen { get; protected set; }// ü�� ȸ����
    public float HealthRegenAscent { get; protected set; }// ������ ü�� ȸ����

    protected virtual void OnEnable()
    {
        IsDeath = false;
        // MaxHealth = data.health;
        Health = MaxHealth;
    }

    /// <summary>
    /// ������ ���� �� ���
    /// </summary>
    /// <param name="damage"></param>
    public virtual void OnDamage(float damage)
    {
        float damageMultiplier = 1 - Armor / (100 + Mathf.Abs(Armor));
        damage *= damageMultiplier;

        Health -= damage;

        if (Health <= 0 && !IsDeath)
        {
            Die();
        }
    }

    /// <summary>
    /// OnDamage �ȿ� ����ִ� �޼ҵ� / OnDeath()���� / ���� : Health <=0 && !IsDeath
    /// </summary>
    public virtual void Die()
    {
        if (OnDeath != null)
        {
            OnDeath();
        }
        IsDeath = true;
    }
}