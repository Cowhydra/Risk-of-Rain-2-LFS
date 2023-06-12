using UnityEngine;

public class Exp : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] private float _moveSpeed = 5.0f;
    GameObject _Player;
    private float _Worth = 25.0f;
    private void OnEnable()
    {
        _Worth = 20 + Random.Range(30, 50) * 10 * (1 + (int)Managers.Game.Difficulty);
    }
    private void Awake()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");

    }
    void Update()
    {
        Vector3 direction = _Player.transform.position - transform.position;
        Vector3 movement = direction.normalized * _moveSpeed * Time.deltaTime;
        transform.position += movement;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStatus>().IncreaseExp(_Worth);
            Debug.Log("����ġ�� ȹ���Ͽ��� ��� ��Ÿ���� �Ҹ��� ����");
            Managers.Resource.Destroy(gameObject);
        }
    }
}
