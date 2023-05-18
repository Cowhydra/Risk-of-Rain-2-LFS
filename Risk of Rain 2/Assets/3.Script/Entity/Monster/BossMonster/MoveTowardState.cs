using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardState : MonoBehaviour, IMonsterBehavior
{
    public void MoveToward()
    {
        Debug.Log("�̹� �̵���");
    }

    public void Idle()
    {
        Debug.Log("�̵��ϴٰ� ���߱�");
    }

    public void Paralysis()
    {
        Debug.Log("�̵��ϴٰ� ����ɸ���");
    }

    public void Aiming()
    {
        Debug.Log("�̵��ϴٰ� �÷��̾� �������");
    }

    public void SkillA()
    {
        Debug.Log("�̵��ϴٰ� ��ųA ����ϱ�");
    }

    public void SkillB()
    {
        Debug.Log("�̵��ϴٰ� ��ųB ����ϱ�");
    }

    public void SkillC()
    {
        Debug.Log("�̵��ϴٰ� ��ųC ����ϱ�");
    }

}
