using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSkill : MonoBehaviour
{
    private BeetleQueen _beetleQueen;
    [SerializeField] private GameObject _beetleQueenObject;
    private float _shootingSpeed = 20f;
    private float _damage = 0f; // ���ݷ��� 130%

    private void OnEnable()
    {
        _beetleQueen = FindObjectOfType<BeetleQueen>();
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
        _beetleQueen.AcidBallPool.ReturnObject(gameObject);
    }

    private void OnParticleCollision(GameObject collObj)
    {
        if (collObj != _beetleQueenObject)
        {   
            if(collObj.CompareTag("Player"))
            {
                Debug.Log("�÷��̾� �ƾ�");
                collObj.GetComponent<Entity>().OnDamage(_damage);
            }
            else
            {
                GameObject obj = _beetleQueen.AcidPoolPool.GetObject();
                obj.transform.position = collObj.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                // TODO : �꼺������ ���� / 15�� ����
            }
            // TODO : ������ ������� ȿ�� �ڷ�ƾ���� �ֱ�
            _beetleQueen.AcidBallPool.ReturnObject(gameObject);
        }
    }
}
