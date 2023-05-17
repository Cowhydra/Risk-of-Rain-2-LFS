using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleQueen : Entity
{
    [SerializeField] private MonsterData _beetleQueenData;
    private Animator _beetleQueenAnimator;

    private Transform _playerTransform; // �÷��̾� ���� �����ϱ� ������ �ʿ�

    private void Awake()
    {
        TryGetComponent<Animator>(out _beetleQueenAnimator);
    }

    protected override void OnEnable()
    {
        SetUp(_beetleQueenData);
        base.OnEnable();
    }

    private void SetUp(MonsterData data)
    {
        _damage = data.Damage;
        _speed = data.Speed;
        _defense = data.Defense;
        _maxHealthAscent = data.MaxHealthAscent;
        _damageAscent = data.DamageAscent;
        _healthRecovery = data.HealthRecovery;
        _recoveryAscent = data.RecoveryAscent;
    }


}
