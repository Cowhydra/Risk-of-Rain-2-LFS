using System.Collections.Generic;
using UnityEngine;

public class ItemPrimitiive : MonoBehaviour
{
    protected GameObject Player;
    protected GameObject Target;
    protected PlayerStatus _playerStatus;
    public virtual void Init()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        _playerStatus = Player.GetComponent<PlayerStatus>();
    }
    private void Start()
    {
        Init();
    }
    protected virtual void ShowDamageUI(GameObject TargetObject, float damage, Define.EDamageType DamageType = Define.EDamageType.Nomal)
    {
        DamageUI _damageUI = Managers.UI.MakeWorldSpaceUI<DamageUI>();
        _damageUI.transform.SetParent(TargetObject.transform);
        _damageUI.transform.localPosition = Vector3.zero;
        _damageUI.SetDamage(damage);
        _damageUI.Excute();

        switch (DamageType)
        {
            case Define.EDamageType.Nomal:
                break;
            case Define.EDamageType.Cirtical:
                _damageUI.SetColor(Color.red);
                break;
            case Define.EDamageType.Item:
                _damageUI.SetColor(Color.blue);
                break;
        }

    }
    //���ͽ�Ʈ�� �˰��� �ִ� �Ÿ� ���� ã�� �˰����̳� ���� ������� ����!
    protected virtual GameObject FindClosestEnemy()
    {
        List<GameObject> enemies = new List<GameObject>();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Monster"));
        enemies.AddRange(GameObject.FindGameObjectsWithTag("BeetleQueenMouth"));

        if (enemies.Count == 0)
        {
            return null; // ���� ���� ��� null ��ȯ
        }

        GameObject closestEnemy = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            Vector3 direction = enemy.transform.position - Player.transform.position;
            float distanceSqr = direction.sqrMagnitude;

            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}
