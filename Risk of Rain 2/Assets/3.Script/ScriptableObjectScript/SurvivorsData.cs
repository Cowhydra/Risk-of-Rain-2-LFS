using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SurvivorsData", fileName = "survivorsData")]
public class SurvivorsData : ScriptableObject
{
    public string Name;
    public float MaxHealth;
    public float Damage;
    public float HealthRegen;
    public float Armor;
    public float MoveSpeed;
    public float Mass;
    public float CriticalChance;
    public int MaxJumpCount;

    //������ ���� ��������

    public float HealthAscent;
    public float DamageAscent;
    public float HealthRegenAscent;
}
