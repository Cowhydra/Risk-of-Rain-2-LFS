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
    private Transform _cameraTransform;

    //�÷��̾� ������Ʈ
    private Animator _playerAnimator;
    private Rigidbody _playerRigidbody;
    private PlayerInput _playerInput;
    private PlayerStatus _playerStatus;

    //�÷��̾� �������ͽ�
    private readonly WaitForSeconds _jumpCheckTime = new WaitForSeconds(0.02f);
    private readonly float _massCoefficient = 150f;
    private float _jumpForce = 200f;
    private int _jumpCount;
    private bool _isJumping;
    private float _rotateSpeed = 550f;
    private bool _isSprinting;

    //Ground Check
    private readonly float _groundCheckDistance = 0.11f;
    private readonly float _yOffset = 0.1f;

    //�̵��� ������ �ִ� ������ ����
    private int _bonusJumpCount = 0;
    private float _bonusMoveSpeed = 1f;

    private void Awake()
    {
        TryGetComponent(out _playerAnimator);
        TryGetComponent(out _playerRigidbody);
        TryGetComponent(out _playerInput);
        TryGetComponent(out _playerStatus);
    }
    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        _jumpForce *= _massCoefficient / _playerStatus.Mass;
        _jumpCount = _playerStatus.MaxJumpCount;
    }
    private void Update()
    {
        CheckGround();
        CheckSprint();
        Rotate();
       
        if (_playerInput.Jump)
        {
            Jump();
        }

    }

    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        Vector3 _moveDirection;
        Vector3 _distance;
        Vector2 _move = new Vector2(_playerInput.HorizontalDirection, _playerInput.Move);
        _moveDirection = _move.x * transform.right + _move.y * transform.forward;
        if (_isSprinting)
        {
            _distance = 1.5f * _playerStatus.MoveSpeed * _bonusMoveSpeed * Time.deltaTime * _moveDirection.normalized;
        }
        else
        {
            _distance = _playerStatus.MoveSpeed * _bonusMoveSpeed * Time.deltaTime * _moveDirection.normalized;
        }
        _playerRigidbody.MovePosition(_playerRigidbody.position + _distance);

        if (_isSprinting)
        {
            _playerAnimator.SetFloat("Move", 1.5f * _playerInput.Move);
        }
        else
        {
            _playerAnimator.SetFloat("Move", _playerInput.Move);
        }
        _playerAnimator.SetFloat("Horizon", _playerInput.HorizontalDirection);
    }

    private void Rotate()
    {
        Quaternion _targetRotaition = Quaternion.Euler(0, _cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotaition, _rotateSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (_jumpCount > 0)
        {
            StartCoroutine(CheckJump_co());
            if (_isJumping)
            {
                _playerRigidbody.velocity = Vector3.zero;
            }
            _playerAnimator.SetBool("Jump", true);
            _playerRigidbody.AddForce(Vector3.up * _jumpForce);
            _jumpCount--;
        }
    }

    private IEnumerator CheckJump_co()
    {
        yield return _jumpCheckTime;
        _isJumping = true;
    }

    private void CheckGround()
    {
        if (Physics.Raycast(transform.position + new Vector3(0, _yOffset, 0), Vector3.down, out _, _groundCheckDistance) && _isJumping)
        {
            _playerAnimator.SetBool("Jump", false);
            _jumpCount = _playerStatus.MaxJumpCount + _bonusJumpCount;
            _isJumping = false;
        }
    }

    private void CheckSprint()
    {
        if (_playerInput.Sprint && _playerInput.Move > 0)
        {
            _isSprinting = !_isSprinting;
        }

        if (_playerInput.Move <= 0)
        {
            _isSprinting = false;
        }
    }
}