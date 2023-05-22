using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator _playerAnimator;

    private WaitForSeconds _attackSavingTime = new WaitForSeconds(0.32f);
    public Coroutine RunningCoroutine;
    public bool IsAttacking;
    public int AttackCount = 1;

    //��ƿ��Ƽ ��ų ����
    [SerializeField] private float _dashDistance;
    [SerializeField] private float _dashSpeed;
    private Rigidbody _playerRigidbody;
    private CinemachineFreeLook _virtualCamera;
    private Transform _cameraTransform;
    private void Awake()
    {
        TryGetComponent(out _playerAnimator);
        TryGetComponent(out _playerRigidbody);
        _virtualCamera = FindObjectOfType<CinemachineFreeLook>();
        _cameraTransform = Camera.main.transform;
    }

    public void Attack1()
    {
        IsAttacking = true;
        _playerAnimator.SetBool("Attack1", true);
        AttackCount++;
    }
    public void Attack2()
    {
        IsAttacking = true;
        _playerAnimator.SetBool("Attack2", true);
        AttackCount++;
        StopCoroutine(RunningCoroutine);
    }
    public void Attack3()
    {
        IsAttacking = true;
        _playerAnimator.SetBool("Attack3", true);
        AttackCount++;
        StopCoroutine(RunningCoroutine);

    }
    public IEnumerator AttackTimeCheck_co()
    {
        yield return _attackSavingTime;
        _playerAnimator.SetBool("Attack3", false);
        _playerAnimator.SetBool("Attack2", false);
        _playerAnimator.SetBool("Attack1", false);
        AttackCount = 0;
    }

    private void AnimationEnd()
    {
        IsAttacking = false;
        RunningCoroutine = StartCoroutine(AttackTimeCheck_co());
    }
    private void EndAttack()
    {
        _playerAnimator.SetBool("Attack3", false);
        _playerAnimator.SetBool("Attack2", false);
        _playerAnimator.SetBool("Attack1", false);
        AttackCount = 0;
        IsAttacking = false;
    }

    /// <summary>
    /// �뽬 �Ÿ��� ������ �Ű�, ���������ְ�, ��ο� �����ϴ� ���Ϳ��� ����� ������,
    /// ���� ������ �� �� �� ��밡�� �ִ� 3ȸ
    /// prep - loop - exit
    /// ����������, ���Ϳ��� ������
    /// --�÷��̾ Ʈ���ŷ� �ٲ۴�. ontriggerEnter - ����(���߿�), ������
    /// </summary>
    /// <returns></returns>

    private IEnumerator Dash_co()
    {
        Vector3 _destPos = transform.position + (transform.position - _virtualCamera.transform.position) * _dashDistance;
        while(Vector3.SqrMagnitude(transform.position - _destPos) >= 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _destPos, _dashSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = _destPos;  
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            other.GetComponent<Entity>().OnDamage(10);
        }
    }
}