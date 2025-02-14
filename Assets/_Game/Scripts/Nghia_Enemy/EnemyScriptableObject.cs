using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObject/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("General Stats")]
    public string enemyName;
    public float moveSpeed;
    public float maxHealth;
    public bool isRanged; // Nếu true thì là enemy bắn xa, false thì là cận chiến.

    [Header("Melee Enemy Stats")]
    public float meleeDamage;

    [Header("Ranged Enemy Stats")]
    public float attackCooldown;
    public float attackRange;
    public GameObject bulletPrefab;
}
