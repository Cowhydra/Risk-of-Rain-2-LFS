using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float MaxHealth; // ������ ���� �þ
    [SerializeField]
    public float Health { get; protected set; }
    [SerializeField]
    public bool IsDeath { get; protected set; }
    public event Action OnDeath;

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