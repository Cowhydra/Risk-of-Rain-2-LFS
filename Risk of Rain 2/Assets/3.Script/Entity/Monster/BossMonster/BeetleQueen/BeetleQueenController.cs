using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleQueenController : MonoBehaviour
{
    private BeetleQueen _beetleQueen;
    private Animator _beetleQueenAnimator;

    private Transform _player;

    // StartAcidBileSkill() 10��
    // StartWardSkill() 18�� ü�� / 50% �̸��϶�
    // StartRangeBombSkill() 20�� ü�� / 30% �̸��϶�
    private float[] _skillCoolDownArr = new float[3]; // 10 18 20
    private bool[] _isSkillRun = new bool[3];

    private enum RotationAngle { LEFT45, LEFT90, LEFT135, RIGHT45, RIGHT90, RIGHT135}
    private enum BossState { IDLE, WALK, ROTATING, USINGSKILL}

    private RotationAngle _currentRotationAngle;
    private BossState _currentState = BossState.IDLE;

    private bool _playerInFieldOfView = false;

    private void Awake()
    {
        _beetleQueen = FindObjectOfType<BeetleQueen>();
        TryGetComponent(out _beetleQueenAnimator);
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _skillCoolDownArr[0] = 10f;
        _skillCoolDownArr[1] = 15f;
        _skillCoolDownArr[2] = 20f;

        for (int i = 0; i < _isSkillRun.Length; i++)
        {
            _isSkillRun[i] = false;
        }
    }

    private void Update()
    {
        switch (_currentState)
        {
            case BossState.IDLE:
                if (_isSkillRun[0] && IsPlayerInFieldOfView())
                {
                    ChangeState(BossState.USINGSKILL);
                    UseSkill(0); //��ų1 �ߵ� SetTrigger("FireSpit");
                }
                else if (_isSkillRun[0] && !IsPlayerInFieldOfView())
                {
                    ChangeState(BossState.ROTATING);
                }
                else if (_isSkillRun[1] && IsPlayerBehindBoss())
                {
                    ChangeState(BossState.USINGSKILL);
                    UseSkill(1); //��ų2�ߵ� SetTrigger("SpawnWard");
                }
                else if (_isSkillRun[1] && !IsPlayerBehindBoss())
                {
                    ChangeState(BossState.ROTATING);
                }
                else if (_isSkillRun[2])
                {
                    ChangeState(BossState.USINGSKILL);
                    UseSkill(2); //��ų3�ߵ� SetTrgger("RangeBomb");
                }
                else
                {
                    ChangeState(BossState.WALK); // SetTrigger("Walk");
                }
                break;
            case BossState.WALK:
                MoveTowardsPlayer(); // SetTrigger("Walk")
                break;
            case BossState.ROTATING:
                // SetTrigger("Aiming")
                break;
            case BossState.USINGSKILL:
                if (!_beetleQueen.IsRun) // ��ų ���� ������ �ٲ� (��Ÿ�� �ƴ� ����)
                {
                    ChangeState(BossState.IDLE);
                }
                break;
        }
    }

    private bool IsPlayerInFieldOfView() // �÷��̾ ���� �þ߿� �ִ��� ������ �Ǵ��ϴ� �޼ҵ�
    {
        return true;
    }

    private bool IsPlayerBehindBoss() // �÷��̾ ���� ���ʿ� �ִ��� ������ �Ǵ��ϴ� �޼ҵ� / �����ؾ���
    {
        return true;
    }

    private void ChangeState(BossState newState)
    {
        _currentState = newState;

        if(_currentState == BossState.ROTATING)
        {
            _beetleQueenAnimator.SetTrigger("Aiming");
            CalculateAngle(); // ���� ���
            // ���� ȸ���ϴ� �ִϸ��̼��� ������ IDLE�� ���� �ٲ������
        }
    }

    private void CalculateAngle()
    {
        // ���� ��� �ϰ� ��ŭ ȸ�� ��ų���� �ִϸ��̼� ���� / SetTrigger("Left45");
    }

    private void MoveTowardsPlayer() // ������ �÷��̾� ���� �ȴ� �޼ҵ�
    {
        _beetleQueenAnimator.SetTrigger("Walk"); // �̵��� �ִϸ����Ϳ� �̺�Ʈ�� ����
        // Has Exit Time üũ -> Idle �ִϸ��̼����� �Ѿ
        // �ִϸ��̼� ������ IDLE�� ���´� �ٲ������
    }
    private void UseSkill(int skillIndex) // ��ų ���
    {
        StartCoroutine(UseSkill_co(skillIndex));
    }

    private IEnumerator UseSkill_co(int skillIndex)
    {
        switch (skillIndex)
        {
            case 0:
                _beetleQueenAnimator.SetTrigger("FireSpit"); // ��ų�� �ִϸ����Ϳ� �̺�Ʈ�� ����
                _isSkillRun[skillIndex] = true;
                break;
            case 1:
                _beetleQueenAnimator.SetTrigger("SpawnWard"); // ��ų�� �ִϸ����Ϳ� �̺�Ʈ�� ����
                _isSkillRun[skillIndex] = true;
                break;
            case 2:
                _beetleQueenAnimator.SetTrigger("RangeBomb"); // ��ų�� �ִϸ����Ϳ� �̺�Ʈ�� ����
                _isSkillRun[skillIndex] = true;
                break;
        }
        yield return new WaitForSeconds(_skillCoolDownArr[skillIndex]); // ��Ÿ�Ӹ�ŭ ��ٸ���
        _isSkillRun[skillIndex] = false; // ��ų ��Ÿ�� �� ������
    }
}
