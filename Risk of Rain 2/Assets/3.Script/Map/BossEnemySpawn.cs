using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemySpawn : MonoBehaviour
{
    [Header("��ȯ�� ����")]
    [SerializeField] GameObject[] _monsterPrefab;
    [Header("�������� ��ġ")]
    [SerializeField] Transform[] _spawnPoint;
    [Header("���� Ÿ��")]
    [SerializeField] float _spawnTime;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(BossEnemy_co());
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(BossEnemy_co());
    }

    private IEnumerator BossEnemy_co()
    {
        WaitForSeconds wfs = new WaitForSeconds(_spawnTime);
        
        while (true)
        {
            int randomSpawnPoint = Random.Range(0, _spawnPoint.Length);
            int randomMonster = Random.Range(0, _monsterPrefab.Length);

            GameObject randomMonsterPrefab = _monsterPrefab[randomMonster];
            Transform randomPoint = _spawnPoint[randomSpawnPoint];

            Instantiate(randomMonsterPrefab, randomPoint.position, randomPoint.rotation);
            yield return wfs;
        }
    }
}
