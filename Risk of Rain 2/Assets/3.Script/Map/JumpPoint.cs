using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPoint : MonoBehaviour
{
    [SerializeField] private GameObject _jumpObj;  //�����ϴ� ��� (�ν����� â���� ���� �ʿ����. �Ʒ� �ڵ�� ����. ���� ����������Ʈ)
    [SerializeField] private GameObject _highPoint;  //���� �� �ִ� ���� ���� (�ν����� â���� �־����)
    [SerializeField] private GameObject _goalPoint;  //���� �� ������ (�ν����� â���� �־����)

    [Header("�����ӵ� ����")]
    [SerializeField][Range(0.001f, 1f)] float jumpSpeed = 0.001f;    //���������� �̵��ϴ� �ӵ�

    Rigidbody jumpRigidbody;      //�������� Rigidbody Gravity�� ���ֱ� ����.


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("����");
        _jumpObj = other.gameObject;     //�����ϴ� ���ӿ�����Ʈ�� ����������Ʈ�� �����Ѵ�.
        jumpRigidbody = _jumpObj.GetComponent<Rigidbody>();

        StartCoroutine(JumpGear_co());     //�����ϸ� ������� �ڷ�ƾ ����
        Debug.Log("�ڷ�ƾ ����");
    }



    IEnumerator JumpGear_co()
    {
        Vector3 goalPos = _goalPoint.transform.position;     //��������, �ν�����â���� �߰��� ������Ʈ�� ��ġ�� �޾ƿ´�.
        Vector3 highPos = _highPoint.transform.position;     //�ִ� ��������

        //while���� SqrMagnitude �޼ҵ带 ���ؼ� �����ϴ� ������Ʈ�� ���������� �Ÿ��� Ȯ���ϰ�, �� �Ÿ��� 0.05f���� �ָ� ���� ������Ʈ�� �߷��� false�� �����

        while (Vector3.SqrMagnitude(_jumpObj.transform.position - highPos) >= 0.05f)
        {
            jumpRigidbody.useGravity = false;
            _jumpObj.transform.position = Vector3.Slerp(_jumpObj.transform.position, highPos, jumpSpeed);

            if (Vector3.SqrMagnitude(_jumpObj.transform.position - highPos) <= 0.05f)
            {
                Debug.Log("�߰�");
                jumpRigidbody.useGravity = true;

                //while (Vector3.SqrMagnitude(jumpObj.transform.position - goalPos) >= 0.05f)
                //{
                //    Debug.Log("������");
                //    //*yield return null;
                //    //*jumpRigidbody.useGravity = false;
                //    //Slerp + transform.position�� ���� ���������� �̵��ϸ� ������������ �̵��Ѵ�. ���� ������Ʈ�� ���������� �Ÿ��� 0.05f �̻��̸� �ڵ带 �ݺ��ؼ� �̵��ϰ� �ȴ�.
                //    jumpObj.transform.position = Vector3.Slerp(jumpObj.transform.position, goalPos, jumpSpeed);
                //}
                //jumpObj.transform.position = goalPos;
                //*jumpRigidbody.useGravity = true;
                yield return null;
            }
        }
    }


}