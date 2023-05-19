using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPoint : MonoBehaviour
{
    [SerializeField] GameObject jumpObj;  //�����ϴ� ���(�ν����� â���� ���� �ʿ����. �Ʒ� �ڵ�� ����. ���� ����������Ʈ)
    [SerializeField] GameObject goalPoint;  //���� �� ������(�ν����� â���� �־����)

    [Header("�����ӵ� ����")]
    [SerializeField] [Range(0.001f, 1f)] float jumpSpeed = 0.5f;    //���������� �̵��ϴ� �ӵ�

    bool isJumping = false;     //����������Ʈ�� ���� ���¸� Ȯ���ϱ� ����
    Rigidbody jumpRigidbody;      //�������� Rigidbody Gravity�� ���ֱ� ����.


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("����");
        jumpObj = other.gameObject;     //�����ϴ� ���ӿ�����Ʈ�� ����������Ʈ�� �����Ѵ�.
        jumpRigidbody = jumpObj.GetComponent<Rigidbody>();
        
        StartCoroutine(JumpGear());     //�����ϸ� ������� �ڷ�ƾ ����
        Debug.Log("�ڷ�ƾ ����");

    }

    

    IEnumerator JumpGear()
    {
        isJumping = true;
        Vector3 goalPos = goalPoint.transform.position;
        
        while (Vector3.SqrMagnitude(jumpObj.transform.position - goalPos)>= 0.05f)
        {
            yield return null;
            jumpRigidbody.useGravity = false;
            jumpObj.transform.position = Vector3.Slerp(jumpObj.transform.position, goalPos, jumpSpeed);
        }
        jumpObj.transform.position = goalPos;
        jumpRigidbody.useGravity = true;
        isJumping = false;

        //    while (isJumping)    //��ǥ������ �����Ҷ����� �ݺ��Ѵ�
        //    {
        //        yield return null;
        //        jumpRigidbody.useGravity = false;     //�ε巯�� ������ ���� ���� ����, ���� ������Ʈ�� �߷��� ���ش�.
        //        Vector3 goalPos = goalPoint.transform.position;
        //        jumpObj.transform.position = Vector3.Slerp(jumpObj.transform.position, goalPos, jumpSpeed);     //SLerp�� �̿��Ͽ� �������� �׸��� �������� �����ϰ� �ȴ�.
        //        
        //        float distance = Vector3.Distance(jumpObj.transform.position, goalPos);
        //        if (distance < 0.03f)
        //        {
        //            Debug.Log("�ȴ�");
        //            jumpRigidbody.useGravity = true;
        //            isJumping = false;
        //        }
        //    }
    }




}
