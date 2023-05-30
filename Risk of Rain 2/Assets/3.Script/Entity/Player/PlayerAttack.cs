using Cinemachine;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator _playerAnimator;
    private PlayerInput _playerInput;
    private Rigidbody _playerRigidbody;

    private WaitForSeconds _attackSavingTime = new WaitForSeconds(0.32f);
    public Coroutine RunningCoroutine;
    public bool _isAttacking;
    private int _attackCount = 0;

    //��ƿ��Ƽ ��ų ����
    [SerializeField] private float _dashDistance;
    [SerializeField] private float _dashSpeed;
    private CinemachineFreeLook _virtualCamera;
    private Transform _cameraTransform;
    private void Awake()
    {
        TryGetComponent(out _playerAnimator);
        TryGetComponent(out _playerRigidbody);
        TryGetComponent(out _playerInput);
        _virtualCamera = FindObjectOfType<CinemachineFreeLook>();
        _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        Debug.Log(_playerInput.Mouse1);
        if (_playerInput.Mouse1 && _attackCount <= 2 && !_isAttacking)
        {
            if (_attackCount == 0)
            {
                Attack1();
            }
            else if (_attackCount == 1)
            {
                Attack2();
            }
            else if (_attackCount == 2)
            {
                Attack3();
            }
        }
        if (_playerInput.Shift)
        {
            StartCoroutine(Dash_co());
        }
    }
    public void Attack1()
    {
        _isAttacking = true;
        _playerAnimator.SetBool("Attack1", true);
        _attackCount++;
    }
    public void Attack2()
    {
        _isAttacking = true;
        _playerAnimator.SetBool("Attack2", true);
        _attackCount++;
        if (RunningCoroutine != null)
        {
            StopCoroutine(RunningCoroutine);
        }
    }
    public void Attack3()
    {
        _isAttacking = true;
        _playerAnimator.SetBool("Attack3", true);
        _attackCount++;
        if (RunningCoroutine != null)
        {
            StopCoroutine(RunningCoroutine);
        }

    }
    public IEnumerator AttackTimeCheck_co()
    {
        yield return _attackSavingTime;
        _playerAnimator.SetBool("Attack3", false);
        _playerAnimator.SetBool("Attack2", false);
        _playerAnimator.SetBool("Attack1", false);
        _attackCount = 0;
    }

    private void AnimationEnd()
    {
        _isAttacking = false;
        RunningCoroutine = StartCoroutine(AttackTimeCheck_co());
    }
    private void EndAttack()
    {
        _playerAnimator.SetBool("Attack3", false);
        _playerAnimator.SetBool("Attack2", false);
        _playerAnimator.SetBool("Attack1", false);
        _attackCount = 0;
        _isAttacking = false;
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
        //Vector3 _destPos = transform.position + (transform.position - _virtualCamera.transform.position) * _dashDistance;
        Vector3 _destPos = transform.position + _cameraTransform.forward * _dashDistance;
        while (Vector3.SqrMagnitude(transform.position - _destPos) >= 0.001f)
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