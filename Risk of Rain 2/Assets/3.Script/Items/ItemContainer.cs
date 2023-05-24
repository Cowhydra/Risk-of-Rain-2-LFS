using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    public int price;

    private void Start()
    {
        price = Random.Range(25, 50);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("UI ����� ");
            Debug.Log("��ȣ�ۿ� Ű�� ������ �������� ȹ�� �� �ƴ϶� ���� ");
            Debug.Log("���ڷκ��� ���� ��ġ ���� �ε�, ���߿� �����̰� ����Ʈ�� ������ ���� �� �ڽ� ������Ʈ ���� ��" +
                "���������� �����ؼ� �� ������ ������? ");
            Debug.Log("�������� �����ǰ�, �������� Collider�� ������ �־ �ش� ������ Collider�� ���� �������� ȹ��");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
    }

}
