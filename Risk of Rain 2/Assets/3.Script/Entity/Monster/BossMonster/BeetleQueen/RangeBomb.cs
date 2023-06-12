using System.Collections;
using UnityEngine;

public class RangeBomb : MonoBehaviour
{
    private BeetleQueen _beetleQueen;
    private GameObject _player;
    private bool _isAddForce = false;
    private float _force = 1000f;
    private float _damage = 0;
    private void OnEnable()
    {
        StartCoroutine(DeleteRangeBomb_co());
        _beetleQueen = FindObjectOfType<BeetleQueen>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        if (_beetleQueen != null)
        {
            _damage = _beetleQueen.Damage * 0.8f;
        }
    }
    private IEnumerator DeleteRangeBomb_co()
    {
        yield return new WaitForSeconds(5f);
        if (_isAddForce)
        {
            Debug.Log("�÷��̾ ��Ʋ���� RangeBomb ���� ���� damage : " + _damage);
            Debug.Log("�÷��̾� Hit Sound�� ����");
            _player.GetComponent<Rigidbody>().AddForce(Vector3.up * _force);
            _player.GetComponent<Entity>().OnDamage(_damage);
        }
        Debug.Log("BeetleQueen�� RangeBomb�� ������ �Ҹ��� ����");
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _isAddForce = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _isAddForce = false;
        }
    }
}