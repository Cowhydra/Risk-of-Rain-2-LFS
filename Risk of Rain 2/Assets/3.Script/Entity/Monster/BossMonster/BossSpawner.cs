using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private BeetleQueen _beetleQueen;

    public void Spawn() // TODO : �÷��̾ �ڷ���Ʈ �۵� �������� Spawn�ؾ���
    {
        BeetleQueen beetleQueen = Instantiate(_beetleQueen, transform.position, transform.rotation);

        beetleQueen.OnDeath += () => Destroy(_beetleQueen.gameObject, 10f);
    }
}
