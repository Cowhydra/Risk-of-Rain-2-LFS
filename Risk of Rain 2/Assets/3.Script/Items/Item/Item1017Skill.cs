using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1017Skill : ItemPrimitiive
{
    Dictionary<int, GameObject> enemyTagert = new Dictionary<int, GameObject>();
    void Start()
    {
        gameObject.transform.position = Player.transform.position;   
    }

    //1. �� ���� ü�� ���̸� �ҷ�����
    //2. �÷��̾ ������ �� ������ ��ü�� �߻��ϰ� �ش� ��ü�� �¾Ƽ� ����� ���  ����ü���� �� �Լ� ���� -> Always�� 


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Boss")||other.CompareTag("Boss")) //��ų�� ���� �ױ� �������� �� �ӽ�, ����  ������Ʈ�� ���������� ���� ����
        {
            if (enemyTagert.ContainsKey(other.GetHashCode()))
            {
                return;
            }
            else
            {
                enemyTagert.Add(other.GetHashCode(), other.gameObject);
            }

        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator Item1017Skill_co()
    {
        yield return new WaitForSeconds(2.0f);
    }
}
