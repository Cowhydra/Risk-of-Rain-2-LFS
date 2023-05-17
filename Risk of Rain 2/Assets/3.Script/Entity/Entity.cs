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
    public float Health { get; protected set; }
    public bool IsDeath { get; protected set; }
    public event Action OnDeath;

    protected float _damage; // ���ݷ�
    protected float _speed; // �ӵ�
    protected float _defense; // ����
    protected float _maxHealthAscent; // ������ ü�� ���ġ
    protected float _damageAscent; // ������ ���ݷ� ���ġ
    protected float _healthRecovery;// ü�� ȸ����
    protected float _recoveryAscent;// ������ ü�� ȸ����

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
        float damageMultiplier = 1 - _defense / (100 + Mathf.Abs(_defense));
        damage += damageMultiplier;

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