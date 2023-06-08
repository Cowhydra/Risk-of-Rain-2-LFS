using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemySpawn : MonoBehaviour
{
    [Header("��ȯ�� ����")]
    [SerializeField] List<GameObject> _monsterPrefab;
    [Header("�������� ��ġ")]
    [SerializeField] Transform[] _spawnPoint;
    [Header("���� Ÿ��")]
    [SerializeField] float _spawnTime;

    private bool _isSpawned = false;

    private void Start()
    {
        _monsterPrefab.Clear();
        _monsterPrefab.Add(Managers.Resource.Load<GameObject>("Prefabs/Imp"));
        _monsterPrefab.Add(Managers.Resource.Load<GameObject>("Prefabs/Lemurian"));
        _monsterPrefab.Add(Managers.Resource.Load<GameObject>("Prefabs/Golem"));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_isSpawned)
            {
                _isSpawned = true;
                StartCoroutine(BossEnemy_co());
            }
        }
     
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player")&&Managers.Game.GameState==Define.EGameState.ActiveTelePort)
        {
            Managers.Game.ProgressBoss += Time.deltaTime;
            Managers.Event.BossProgress?.Invoke();
        }
            
    
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(BossEnemy_co());
            _isSpawned = false;
        }
    }

    private IEnumerator BossEnemy_co()
    {
        while (_isSpawned&&true)
        {
            int randomSpawnPoint = Random.Range(0, _spawnPoint.Length);
            int randomMonster = Random.Range(0, _monsterPrefab.Count);

            GameObject randomMonsterPrefab = _monsterPrefab[randomMonster];
            Transform randomPoint = _spawnPoint[randomSpawnPoint];

            GameObject _enemy = Managers.Resource.Instantiate($"{_monsterPrefab[randomMonster].name}"
                   , _spawnPoint[randomSpawnPoint]);
            yield return new WaitForSeconds(_spawnTime);
        }
        
    }

}
