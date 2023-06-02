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

    private float _health;
    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = Mathf.Clamp(value, 0, MaxHealth);
        }
    }


    public bool IsDeath { get; protected set; }
    public event Action OnDeath;

    public float Damage { get; set; } // ���ݷ�
    public float MoveSpeed { get; set; } // �ӵ�
    public float Armor { get; set; } // ����
    public float MaxHealthAscent { get; protected set; } // ������ ü�� ���ġ
    public float DamageAscent { get; protected set; } // ������ ���ݷ� ���ġ
    public float HealthRegen { get; set; }// ü�� ȸ����
    public float HealthRegenAscent { get; protected set; }// ������ ü�� ȸ����
    private WaitForSeconds _healthRegenDelay = new WaitForSeconds(1f);

    private int _difficulty = 0;
    protected virtual void OnEnable()
    {
        IsDeath = false;
        // MaxHealth = data.health;
        Health = MaxHealth + MaxHealthAscent * _difficulty;
        Damage += DamageAscent * _difficulty;
        HealthRegen += HealthRegenAscent * _difficulty;
        StartCoroutine(RegenerateHealth_co());
    }

    private void Start()
    {
        Managers.Event.DifficultyChange -= SetDifficulty;
        Managers.Event.DifficultyChange += SetDifficulty;
    }

    private void SetDifficulty(int difficulty)
    {
        _difficulty = difficulty;
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
        if (!gameObject.CompareTag("Player"))
        {
            Managers.ItemApply.ExcuteInSkills();
        }

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
        if (!gameObject.CompareTag("Player"))
        {
            Managers.ItemApply.ExcuteAfterSkills(gameObject.transform);
        }

        if (OnDeath != null)
        {
            OnDeath();
        }
        IsDeath = true;
    }

    private IEnumerator RegenerateHealth_co()
    {
        while (true)
        {
            RegenerateHealth();
            yield return _healthRegenDelay;
        }
    }

    protected virtual void RegenerateHealth()
    {
        Health += HealthRegen;
    }
}
