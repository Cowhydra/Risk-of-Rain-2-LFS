using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("��ȯ�� ����")]
    [SerializeField] List<GameObject> _monsterPrefab;
    [Header("�������� ��ġ")]
    [SerializeField] Transform[] _spawnPoint;
    private bool _EnemySpawned = false;

    private void Start()
    {
        _monsterPrefab.Clear();
        _monsterPrefab.Add(Managers.Resource.Load<GameObject>("Prefabs/Imp"));
        _monsterPrefab.Add(Managers.Resource.Load<GameObject>("Prefabs/Lemurian"));
        _monsterPrefab.Add(Managers.Resource.Load<GameObject>("Prefabs/Golem"));

    }

    private void OnTriggerEnter(Collider other)
    {
        int random = Random.Range(0, _spawnPoint.Length);
        Transform randomSpawnPoint = _spawnPoint[random];
     
        int _monsterRandomCount = Random.Range(1,5);
        if (other.CompareTag("Player")&&!_EnemySpawned)
        {
            _EnemySpawned = true;
            Debug.Log("���� ���� ����");
            for(int i = 0; i < _monsterRandomCount; i++)
            {
                int _monsterRandomObject = Random.Range(0, _monsterPrefab.Count);
              GameObject _enemy= Managers.Resource.Instantiate($"{_monsterPrefab[_monsterRandomObject].name}"
                    , randomSpawnPoint.position);
                 _enemy.SetRandomPositionSphere(1, 3, 4, randomSpawnPoint);
            }
         
        }
    }
}
