using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class BeetleQueenControl : MonoBehaviour
{
    private BeetleQueen _beetleQueen;
    private Animator _beetleQueenAnimator;

    private Transform _player;

    private float[] _skillCoolDownArr = new float[3]; // 10 15 20
    private bool[] _isSkillRun = new bool[3];

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
        StartCoroutine(Debug_co());
    }

    private IEnumerator Debug_co()
    {
        while(!_beetleQueen.IsDeath)
        {
            //var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            //var type = assembly.GetType("UnityEditor.LogEntries");
            //var method = type.GetMethod("Clear");
            //method.Invoke(new object(), null);

            //Debug.Log("�÷��̾ �þ߿� " + IsPlayerInFieldOfView());
            //Debug.Log("�÷��̾ �ڿ� " + IsPlayerBehindBoss());
            if(_beetleQueenAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            { 
                // FireSpit SpawnWard RangeBomb
                if(!_isSkillRun[0])
                {
                    if(IsPlayerInFieldOfView())
                    {
                        UseSkill(0);
                        Debug.Log("0�� ��ų ��� / �÷��̾� �þ� �ȿ� ����");
                    }
                    else
                    {
                        Debug.Log("0�� ��ų ��� ���� / �÷��̾� �þ� �ۿ� ����");
                        float angle = CalculateAngle();
                        if(angle < 0)
                        {
                            _beetleQueenAnimator.SetTrigger("Left90");
                        }
                        else
                        {
                            _beetleQueenAnimator.SetTrigger("Right90");
                        }
                    }
                }
                if(!_isSkillRun[1])
                {
                    if(IsPlayerBehindBoss())
                    {
                        UseSkill(1);
                        Debug.Log("1�� ��ų ��� / �÷��̾� �ڿ� ����");
                    }
                    else
                    {
                        Debug.Log("1�� ��ų ��� ���� / �÷��̾� �ڿ� ����");
                        float angle = CalculateAngle();
                        if (angle < 0)
                        {
                            _beetleQueenAnimator.SetTrigger("Left90");
                        }
                        else
                        {
                            _beetleQueenAnimator.SetTrigger("Right90");
                        }
                    }
                }
                if(!_isSkillRun[2])
                {
                    UseSkill(2);
                    Debug.Log("2�� ��ų ���");
                }
                if(_isSkillRun[0] && _isSkillRun[1] && _isSkillRun[2])
                {
                    Debug.Log("��� ��ų ��� �Ұ���");
                    _beetleQueenAnimator.SetTrigger("Aiming");
                }
            }
            yield return null;
        }
    }

    private bool IsPlayerInFieldOfView() // �÷��̾ ���� �þ߿� �ִ��� ������ �Ǵ��ϴ� �޼ҵ�
    {
        float angle = CalculateAngle();
        if (angle >= -45 && angle < 45)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsPlayerBehindBoss() // �÷��̾ ���� ���ʿ� �ִ��� ������ �Ǵ��ϴ� �޼ҵ� / �����ؾ���
    {
        float angle = CalculateAngle();
        if (angle < -135 || angle >= 135)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private float CalculateAngle()
    {
        Vector3 vBase = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
        Vector3 vAnother = new Vector3(_player.transform.position.x - transform.position.x, 0, _player.transform.position.z - transform.position.z).normalized;

        float dot = Vector3.Dot(vBase, vAnother);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        Vector3 cross = Vector3.Cross(vBase, vAnother);
        if(cross.y < 0)
        {
            angle = -angle;
        }
        return angle;
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
        Debug.Log(skillIndex + "�� ��ų �� ������");
    }
}
