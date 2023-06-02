using System.Collections;
using UnityEngine;

public class AcidSkill : MonoBehaviour
{
    private BeetleQueen _beetleQueen;
    [SerializeField] private GameObject _beetleQueenObject;
    private float _shootingSpeed = 40f;
    private float _damage = 0;


    private void OnEnable()
    {
        _beetleQueen = FindObjectOfType<BeetleQueen>();
        StartCoroutine(Shoot_co());
    }

    private void Start()
    {
        if (_beetleQueen != null)
        {
            _damage = _beetleQueen.Damage * 1.3f;
        }
    }

    private IEnumerator Shoot_co() // �߻�
    {
        yield return null;
        float time = 0;
        while (time < 5f)
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
            if (collObj.TryGetComponent(out Entity en))
            {
                if (en.CompareTag("Player"))
                {
                    Debug.Log("�÷��̾ ��Ʋ���� AcidSkill�� ����");
                    Debug.Log("�÷��̾� Hit Sound�� ����");
                    en.OnDamage(_damage);
                    DeleteAcidBile();
                }
            }
            else
            {
                Debug.Log("AcidBall�� AcidPool�� ���ϴ� ����� ���� (������Ʈ�� �ε��� �����ϴ�? ����)");
                GameObject obj = _beetleQueen.AcidPoolPool.GetObject();
                obj.transform.position = collObj.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                DeleteAcidBile();
            }
        }
    }

    //private void OnCollisionEnter(Collision col)
    //{
    //    Debug.Log("ddddd");
    //    if(col.gameObject != _beetleQueenObject)
    //    {
    //        if (col.gameObject.TryGetComponent(out Entity en))
    //        {
    //            if (en.CompareTag("Player"))
    //            {
    //                Debug.Log("�÷��̾ ��Ʋ���� AcidSkill�� ���� ���� damage : " + _damage);
    //                Debug.Log("�÷��̾� Hit Sound�� ����");
    //                en.OnDamage(_damage);
    //                DeleteAcidBile();
    //            }
    //        }
    //        else
    //        {
    //            Debug.Log("AcidBall�� AcidPool�� ���ϴ� ����� ���� (������Ʈ�� �ε��� �����ϴ�? ����)");
    //            GameObject obj = _beetleQueen.AcidPoolPool.GetObject();
    //            ContactPoint contact = col.GetContact(0);
    //            obj.transform.position = contact.point;
    //            obj.transform.up = contact.normal;
    //            DeleteAcidBile();
    //        }
    //    }
    //}
}
