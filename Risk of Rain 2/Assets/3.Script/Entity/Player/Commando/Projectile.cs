using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Rigidbody _projectileRigidbody;
    protected ObjectPool _projectileObjectPool;
    protected string _projectilePoolName;
    protected int _environmentLayer = 6;
    protected float _projectileSpeed;

    private void Awake()
    {
        InitializeProjectile();
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
