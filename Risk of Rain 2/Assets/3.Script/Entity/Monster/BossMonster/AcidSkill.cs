using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSkill : MonoBehaviour
{
    private BeetleQueen _beetleQueen;
    private GameObject _beetleQueenObject;
    private float _shootingSpeed = 20f;
    private float _damage = 0f; // ���ݷ��� 130%

    private void OnEnable()
    {
        _beetleQueen = FindObjectOfType<BeetleQueen>();
        _beetleQueenObject = _beetleQueen.gameObject;
        StartCoroutine(Shoot_co());
    }

    private IEnumerator Shoot_co() // �߻�
    {
        float time = 0;
        while(time < 5f)
        {
            transform.position += transform.forward * _shootingSpeed * Time.deltaTime;
            time += Time.deltaTime;
            yield return null;
        }
        DeleteAcidBile();
    }

    // �꼺���� Ǯ�� ��ȯ
    private void DeleteAcidBile()
    {
        _beetleQueen.objectPool.ReturnObject(gameObject);
    }

    private void OnParticleCollision(GameObject col)
    {
        if (col.gameObject != _beetleQueenObject)
        {   
            if(col.gameObject.CompareTag("Player"))
            {
                Debug.Log("�÷��̾� �ƾ�");
                col.gameObject.GetComponent<Entity>().OnDamage(_damage);
            }
            else
            {
                // TODO : �꼺������ ���� / 17�� ����
            }
            // TODO : ������ ������� ȿ�� �ڷ�ƾ���� �ֱ�
            _beetleQueen.objectPool.ReturnObject(gameObject);
        }
    }
}
