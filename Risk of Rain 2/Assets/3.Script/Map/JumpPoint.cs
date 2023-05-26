using System.Collections;
using UnityEngine;

public class JumpPoint : MonoBehaviour
{
    [SerializeField] GameObject jumpObj;  //�����ϴ� ��� (�ν����� â���� ���� �ʿ����. �Ʒ� �ڵ�� ����. ���� ����������Ʈ)
    [SerializeField] GameObject highPoint;  //���� �� �ִ� ���� ���� (�ν����� â���� �־����)
    [SerializeField] GameObject goalPoint;  //���� �� ������ (�ν����� â���� �־����)

    [Header("�����ӵ� ����")]
    [SerializeField][Range(0.001f, 1f)] float jumpSpeed = 0.001f;    //���������� �̵��ϴ� �ӵ�

    Rigidbody jumpRigidbody;      //�������� Rigidbody Gravity�� ���ֱ� ����.


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("����");
        jumpObj = other.gameObject;     //�����ϴ� ���ӿ�����Ʈ�� ����������Ʈ�� �����Ѵ�.
        jumpRigidbody = jumpObj.GetComponent<Rigidbody>();

        StartCoroutine(JumpGear_co());     //�����ϸ� ������� �ڷ�ƾ ����
        Debug.Log("�ڷ�ƾ ����");
    }



    IEnumerator JumpGear_co()
    {
        Vector3 goalPos = goalPoint.transform.position;     //��������, �ν�����â���� �߰��� ������Ʈ�� ��ġ�� �޾ƿ´�.
        Vector3 highPos = highPoint.transform.position;     //�ִ� ��������

        //while���� SqrMagnitude �޼ҵ带 ���ؼ� �����ϴ� ������Ʈ�� ���������� �Ÿ��� Ȯ���ϰ�, �� �Ÿ��� 0.05f���� �ָ� ���� ������Ʈ�� �߷��� false�� �����

        while (Vector3.SqrMagnitude(jumpObj.transform.position - highPos) >= 0.05f)
        {
            jumpRigidbody.useGravity = false;
            jumpObj.transform.position = Vector3.Slerp(jumpObj.transform.position, highPos, jumpSpeed);

            if (Vector3.SqrMagnitude(jumpObj.transform.position - highPos) <= 0.05f)
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
