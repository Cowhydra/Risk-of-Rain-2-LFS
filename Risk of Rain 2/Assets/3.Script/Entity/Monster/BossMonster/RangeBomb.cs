using System.Collections;
using UnityEngine;

public class RangeBomb : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DeleteRangeBomb_co());
    }

    private IEnumerator DeleteRangeBomb_co()
    {
        yield return new WaitForSeconds(5f);
        // ����
        // ���� / �������� 400%
        // �������� Destroy(gameObject);
        // �굵 ������ƮǮ�� �� ���� �ִµ� �ؾ� �ϳ�..? �ؾ� �Ѵٸ� �ٲٱ� / �ٲٴ°� ���ݾ�
    }
}
