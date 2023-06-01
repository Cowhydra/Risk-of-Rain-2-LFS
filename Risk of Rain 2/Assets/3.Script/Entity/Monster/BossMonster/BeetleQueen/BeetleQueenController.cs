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
    private bool _isRotate = false;

    private enum RotationAngle { LEFT45, LEFT90, LEFT135, RIGHT45, RIGHT90, RIGHT135}
    public enum BossState { IDLE, WALK, ROTATING, USINGSKILL}

    private BossState _currentState = BossState.IDLE;

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

    private void OnEnable()
    {
        StartCoroutine(UpdateState_co());
    }

    private IEnumerator UpdateState_co()
    {
        while(!_beetleQueen.IsDeath)
        {
            Debug.Log("BeetleQueen current state : " + _currentState);
            switch (_currentState)
            {
                case BossState.IDLE:
                    if (!_isSkillRun[0] && IsPlayerInFieldOfView())
                    {
                        ChangeState(BossState.USINGSKILL);
                        UseSkill(0); //��ų1 �ߵ� SetTrigger("FireSpit");
                        Debug.Log("1");
                    }
                    else if (!_isSkillRun[1] && IsPlayerBehindBoss())
                    {
                        ChangeState(BossState.USINGSKILL);
                        UseSkill(1); //��ų2�ߵ� SetTrigger("SpawnWard");
                        Debug.Log("2");
                    }
                    else if (!_isSkillRun[0] && !IsPlayerInFieldOfView())
                    {
                        Debug.Log("3");
                        //ChangeState(BossState.ROTATING);
                    }
                    else if (!_isSkillRun[1] && !IsPlayerBehindBoss())
                    {
                        //ChangeState(BossState.ROTATING);
                        Debug.Log("4");
                    }
                    else if (!_isSkillRun[2])
                    {
                        ChangeState(BossState.USINGSKILL);
                        UseSkill(2); //��ų3�ߵ� SetTrgger("RangeBomb");
                        Debug.Log("5");
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
                    if(!_isRotate)
                    {
                        _isRotate = true;
                        _beetleQueenAnimator.SetTrigger("Aiming");
                        yield return null;
                        if (_beetleQueenAnimator.GetCurrentAnimatorStateInfo(0).IsName("BeetleQueenArmature|aimHorizontal"))
                        {
                            if (_beetleQueenAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.97)
                            {
                                float angle = CalculateAngle(transform.forward, _player.transform.position - transform.position);
                                if (angle >= -75 && angle < -45)
                                {
                                    _beetleQueenAnimator.SetTrigger("Left45");
                                }
                                else if (angle >= -105 && angle < -75)
                                {
                                    _beetleQueenAnimator.SetTrigger("Left90");
                                }
                                else if (angle >= -135 && angle < -105)
                                {
                                    _beetleQueenAnimator.SetTrigger("Left135");
                                }
                                else if (angle >= 45 && angle < 75)
                                {
                                    _beetleQueenAnimator.SetTrigger("Right45");
                                }
                                else if (angle >= 75 && angle < 105)
                                {
                                    _beetleQueenAnimator.SetTrigger("Right90");
                                }
                                else if (angle >= 105 && angle < 135)
                                {
                                    _beetleQueenAnimator.SetTrigger("Right135");
                                }
                                //ChangeState(BossState.IDLE);
                                //_isRotate = false;
                            }
                        }
                    }
                    break;
                case BossState.USINGSKILL:
                    yield return null;
                    if (!_beetleQueen.IsRun) // ��ų ���� ������ �ٲ� (��Ÿ�� �ƴ� ����)
                    {
                        ChangeState(BossState.IDLE);
                    }
                    break;
            }
            yield return null;
        }
    }
    // ȸ���ϴ� �ִϸ��̼� ������ ����Idle�� �ٲٰ� bool ���� �ٲٱ�
    // Walk�� ��Ÿ�� �ؾ��ҵ�..? / ���ϴϱ� ��� ��� �ɾ..



    private bool IsPlayerInFieldOfView() // �÷��̾ ���� �þ߿� �ִ��� ������ �Ǵ��ϴ� �޼ҵ�
    {
        float angle = CalculateAngle(transform.forward, _player.transform.position - transform.position);
        if(angle >= -45 && angle < 45)
        {
            Debug.Log("�÷��̾ ���� �þ߿� �ֽ��ϴ�.");
            return true;
        }
        else
        {
            Debug.Log("�÷��̾ ���� �þ߿� �����ϴ�.");
            return false;
        }
    }

    private bool IsPlayerBehindBoss() // �÷��̾ ���� ���ʿ� �ִ��� ������ �Ǵ��ϴ� �޼ҵ� / �����ؾ���
    {
        float angle = CalculateAngle(transform.forward, _player.transform.position - transform.position);
        if (angle < -135 || angle >= 135)
        {
            Debug.Log("�÷��̾ ���� �ڿ� �ֽ��ϴ�.");
            return true;
        }
        else
        {
            Debug.Log("�÷��̾ ���� �ڿ� �����ϴ�.");
            return false;
        }
    }

    private void ChangeState(BossState newState)
    {
        _currentState = newState;
    }

    private float CalculateAngle(Vector3 vStart, Vector3 vEnd)
    {
        // ���� ���
        Vector3 v = vEnd - vStart;

        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    private void MoveTowardsPlayer() // ������ �÷��̾� ���� �ȴ� �޼ҵ�
    {
        _beetleQueenAnimator.SetTrigger("Walk"); // �̵��� �ִϸ����Ϳ� �̺�Ʈ�� ����
        if(_beetleQueenAnimator.GetCurrentAnimatorStateInfo(0).IsName("BeetleQueenArmature|walkForward"))
        {
            if(_beetleQueenAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99)
            {
                ChangeState(BossState.IDLE);
            }
        }
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
