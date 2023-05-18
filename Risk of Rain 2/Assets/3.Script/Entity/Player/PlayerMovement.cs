using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class PlayerMovement : MonoBehaviour
{
    //ĳ���Ͱ� ī�޶������ �ٶ󺸱� ����
    private CinemachineFreeLook _virtualCamera;

    //�÷��̾� ������Ʈ
    private Animator _playerAnimator;
    private Rigidbody _playerRigidbody;
    private PlayerInput _playerInput;
    private PlayerStatus _playerStatus;

    //�÷��̾� �������ͽ�
    private float _jumpForce = 200;
    private readonly float _massCoefficient = 0.01f;
    private int _jumpCount;
    private bool _isJumping;
    private readonly WaitForSeconds _jumpCheckTime = new WaitForSeconds(0.02f);
    public bool IsSprinting;

    //Ground Check
    private readonly float _groundCheckDistance = 0.11f;
    private readonly float _yOffset = 0.1f;

    //�̵��� ������ �ִ� ������ ����
    private int _bonusJumpCount = 0;
    private float _bonusMoveSpeed = 1f;

    private void Awake()
    {
        _virtualCamera = FindObjectOfType<CinemachineFreeLook>();
        TryGetComponent(out _playerAnimator);
        TryGetComponent(out _playerRigidbody);
        TryGetComponent(out _playerInput);
        TryGetComponent(out _playerStatus);
    }
    private void Start()
    {
        _jumpForce *= _playerStatus.Mass * _massCoefficient;
        _jumpCount = _playerStatus.MaxJumpCount;
    }
    private void Update()
    {
        if (Physics.Raycast(transform.position + new Vector3(0, _yOffset, 0), Vector3.down, out _, _groundCheckDistance) && _isJumping)
        {
            _playerAnimator.SetBool("BonusJump", false);
            _playerAnimator.SetBool("Jump", false);
            _jumpCount = _playerStatus.MaxJumpCount + _bonusJumpCount;
            _isJumping = false;
        }
    }
    private void FixedUpdate()
    {
        Move();
        Rotate();
        if(IsSprinting)
        {
            _playerAnimator.SetFloat("Move", 1.5f * _playerInput.Move);
        }
        else
        {
            _playerAnimator.SetFloat("Move", _playerInput.Move);
        }
        _playerAnimator.SetFloat("HorizontalDirection", _playerInput.HorizontalDirection);
    }
    private void Move()
    {
        Vector3 _moveDirection;
        Vector3 _distance;
        _moveDirection = transform.right * _playerInput.HorizontalDirection + transform.forward * _playerInput.Move;
        
        if (IsSprinting)
        {
            _distance = 1.5f * _playerStatus.MoveSpeed * _bonusMoveSpeed * Time.deltaTime * _moveDirection.normalized;
        }
        else
        {
            _distance = _playerStatus.MoveSpeed * _bonusMoveSpeed * Time.deltaTime * _moveDirection.normalized;
        }
        _playerRigidbody.MovePosition(_playerRigidbody.position + _distance);
    }
    private void Rotate()
    {
        Vector3 _dir = transform.position - _virtualCamera.transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(_dir.x, 0, _dir.z));
    }
    /// <summary>
    /// �÷��̾� ���� �޼���
    /// </summary>
    public void Jump()
    {
        if (_jumpCount > 0)
        {
            StartCoroutine(CheckJump_co());
            _playerRigidbody.velocity = Vector3.zero;
            _playerRigidbody.AddForce(Vector3.up * _jumpForce);
            if (_jumpCount == _playerStatus.MaxJumpCount + _bonusJumpCount)
            {
                _playerAnimator.SetBool("Jump", true);
            }
            else
            {
                _playerAnimator.SetBool("BonusJump", true);
            }
            _jumpCount--;
        }
    }
    private IEnumerator CheckJump_co()
    {
        yield return _jumpCheckTime;
        _isJumping = true;
    }
}