using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    protected PlayerStatus _playerStatus;
    protected Rigidbody _projectileRigidbody;
    protected ObjectPool _projectileObjectPool;
    protected Entity _entity;
    protected string _projectilePoolName;
    protected float _projectileSpeed;
    protected float _damage;
    protected float _damageCoefficient;
    protected float _criticalChance;
    private void Awake()
    {
        _playerStatus = FindObjectOfType<PlayerStatus>();
        InitializeProjectile();
    }

    protected virtual void OnEnable()
    {
        _damage = _playerStatus.Damage;
    }

    /// <summary>
    /// �ڽ� Ŭ�������� ����ü Ǯ �̸� �����������
    /// </summary>
    protected virtual void InitializeProjectile()
    {
        TryGetComponent(out _projectileRigidbody);
        FindObjectPool();
    }
   
    /// <summary>
    /// �Ѿ��� �ٶ󺸴� �������� �߻�
    /// </summary>
    public virtual void ShootForward()
    {
        _projectileRigidbody.velocity = this.transform.forward * _projectileSpeed;
    }
    protected void FindObjectPool()
    {
        _projectileObjectPool = GameObject.Find(_projectilePoolName).GetComponent<ObjectPool>();
    }
}
