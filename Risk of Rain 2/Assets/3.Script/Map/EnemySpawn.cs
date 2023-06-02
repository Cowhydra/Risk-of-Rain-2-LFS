using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("��ȯ�� ����")]
    [SerializeField] GameObject _monsterPrefab;
    [Header("�������� ��ġ")]
    [SerializeField] Transform[] _spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        int random = Random.Range(0, _spawnPoint.Length);
        Transform randomSpawnPoint = _spawnPoint[random];

        if (other.CompareTag("Player"))
        {
            Debug.Log("���� ���� ����");

            Instantiate(_monsterPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation); 
        }
    }
}
