using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item1017Skill : ItemPrimitiive
{
    [SerializeField]
    private float _detectorDistance=20f;
    Dictionary<int, GameObject> enemyTagert = new Dictionary<int, GameObject>();
    void Start()
    {
        gameObject.transform.position = Player.transform.position;
       GetComponent<Rigidbody>().velocity = Player.transform.forward * 15f; ;
    }

    //1. �� ���� ü�� ���̸� �ҷ����� -> ��ų �� ������ ��� ü������ ������ ����
    //2. �÷��̾ ������ �� ������ ��ü�� �߻��ϰ� �ش� ��ü�� �¾Ƽ� ����� ���  ����ü���� �� �Լ� ���� -> Always�� 


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) //��ų�� ���� �ױ� �������� �� �ӽ�, ����  ������Ʈ�� ���������� ���� ����
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
    private void OnDisable()
    {
        StopCoroutine(nameof(Item1017Skill_co));
    }
    private IEnumerator Item1017Skill_co()
    {
        while (true)
        {
            if (enemyTagert.Count != 0)
            {
                yield return new WaitForSeconds(2.0f);
            }
            else
            {
               foreach(var go in enemyTagert.Values)
                {
                    if ((go.transform.position - gameObject.transform.position).magnitude < _detectorDistance)
                    {
                        go.GetComponent<MeshRenderer>().enabled = true;
                    }
                }
            }
            yield return new WaitForSeconds(3.0f);
        }

    }
}
