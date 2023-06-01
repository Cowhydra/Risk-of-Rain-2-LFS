using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeBomb : MonoBehaviour
{
    private GameObject _player;
    private bool _isAddForce = false;
    private float _force = 600f;
    private float _damage = 0;
    private void OnEnable()
    {
        StartCoroutine(DeleteRangeBomb_co());
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private IEnumerator DeleteRangeBomb_co()
    {
        yield return new WaitForSeconds(5f);
        if (_isAddForce)
        {
            Debug.Log("�÷��̾� Hit Sound�� ����");
            _player.GetComponent<Rigidbody>().AddForce(Vector3.up * _force);
            _player.GetComponent<Entity>().OnDamage(_damage);
        }
        Debug.Log("BeetleQueen�� RangeBomb�� ������ �Ҹ��� ����");
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