using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private BeetleQueen _beetleQueen;

    public void Spawn() // TODO : �÷��̾ �ڷ���Ʈ �۵� �������� Spawn�ؾ���
    {
        BeetleQueen beetleQueen = Instantiate(_beetleQueen, transform.position, transform.rotation);
    }
}
