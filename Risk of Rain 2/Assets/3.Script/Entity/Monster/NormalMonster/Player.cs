using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(0.0f, 100f)] // ��ũ�� ��
    [SerializeField] // �����̺����� �����ϰ� ����Ƽ�� ��� �� �ֵ��� ����
    private float speed = 0f;  //���� �����ڸ� ���� �� �⺻ private
                               //����Ƽ���� ������ �ۺ����� �� �� ����Ƽ �� ���� �����ϰ� ǥ���
                               // �ڵ�󿡼� �������� �ٲ� �� ����Ƽ���� �Է��� ���� �ʱ�ȭ��
                               // c#�� m_ ���������� �����Ƿ� ������� ������ �ʿ� ����

 
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
            MoveToDir(Vector3.forward);

        if (Input.GetKey(KeyCode.S))
            MoveToDir(Vector3.back);
        if (Input.GetKey(KeyCode.A))
            MoveToDir(Vector3.left);
        if (Input.GetKey(KeyCode.D))
            MoveToDir(Vector3.right);
    }


    public void MoveToDir(Vector3 _dir)
    {
        //Transform�̶� ����Ƽ�� ������ Ŭ������ �����ͼ� ���
        //Vector3 pos = this.GetComponent<Transform>().position; // �ؿ� �� Ǯ�� ��
        Vector3 newPos = this.transform.position;
        newPos = newPos + (_dir * speed * Time.deltaTime); //�������� ������ �ʿ� ���� Time���� ������
        transform.position = newPos;
    }
}
