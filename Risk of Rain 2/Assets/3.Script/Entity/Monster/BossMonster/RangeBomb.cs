using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeBomb : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DeleteRangeBomb_co());
    }

    private IEnumerator DeleteRangeBomb_co()
    {
        yield return new WaitForSeconds (5f);
        Destroy(gameObject);
        //����
        //���� / �������� 400%
    }
}
