using UnityEngine;

public class FireWard : MonoBehaviour
{
    private Lemurian _lemurian;
    [SerializeField] private GameObject _lemurianObject;
    private Rigidbody _fireWardRigidbody;
    private float _shootingSpeed = 45f;
    private float _damage = 0;

    private void Awake()
    {
        TryGetComponent(out _fireWardRigidbody);
    }
    private void OnEnable()
    {
        _lemurian = FindObjectOfType<Lemurian>();
    }

    private void Start()
    {
        if (_lemurian != null)
        {
            _damage = _lemurian.Damage;
        }
    }

    public void Shoot()
    {
        _fireWardRigidbody.velocity = transform.forward * _shootingSpeed;
    }

    private void DeleteFireWard()
    {
        _lemurian.FireWardPool.ReturnObject(gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject != _lemurianObject)
        {
            if (col.gameObject.TryGetComponent(out Entity en))
            {
                if (en.CompareTag("Player"))
                {
                    Debug.Log("�÷��̾ ���������� FireWard�� ���� ���� damage : " + _damage);
                    Debug.Log("�÷��̾� Hit Sound�� ����");
                    col.GetComponent<Entity>().OnDamage(_damage);
                    DeleteFireWard();
                }
            }
            else
            {
                Debug.Log("���������� FireWard�� ���̳� �ٴڿ� ��� �Ҹ��� ����");
                DeleteFireWard();
            }
        }
    }
}
