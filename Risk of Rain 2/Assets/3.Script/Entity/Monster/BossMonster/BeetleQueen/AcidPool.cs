using System.Collections;
using UnityEngine;

public class AcidPool : MonoBehaviour
{
    private BeetleQueen _beetleQueen;
    [SerializeField] private GameObject _beetleQueenObject;

    private float _damage = 0f;

    private bool isRun = false;

    private void OnEnable()
    {
        _beetleQueen = FindObjectOfType<BeetleQueen>();
        StartCoroutine(DeleteAcidPool_co());
    }

    IEnumerator DeleteAcidPool_co()
    {
        yield return new WaitForSeconds(15f);
        DeleteAcidPool();
    }

    private void DeleteAcidPool()
    {
        _beetleQueen.AcidPoolPool.ReturnObject(gameObject);
    }

    private IEnumerator OnDamage_co(Collider col)
    {
        if (col.gameObject != _beetleQueenObject)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                Debug.Log("�÷��̾ BeetleQueen�� AcidPool�� �ǰ�����");
                Debug.Log("�÷��̾� Hit Sound�� ����");
                col.gameObject.GetComponent<Entity>().OnDamage(_damage);
                yield return new WaitForSeconds(0.5f);
                isRun = false;
            }
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (!isRun)
        {
            isRun = true;
            StartCoroutine(OnDamage_co(col));
        }
    }
}