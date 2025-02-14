using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public Transform player;
    public EnemyScriptableObject enemyData;
    public Transform firePoint;

    [HideInInspector] public float currentHealth;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public bool isRanged;
    [HideInInspector] public float attackCooldown;
    [HideInInspector] public float attackRange;
    [HideInInspector] public GameObject bulletPrefab;
    [HideInInspector] public float meleeDamage;

    private EnemyBaseState currentState;

    public MeleeEnemyChaseState meleeChaseState = new MeleeEnemyChaseState();
    public MeleeEnemyAttackState meleeAttackState = new MeleeEnemyAttackState();
    public RangedEnemyApproachState rangedApproachState = new RangedEnemyApproachState();
    public RangedEnemyAttackState rangedAttackState = new RangedEnemyAttackState();
    public EnemyDeadState deadState = new EnemyDeadState();

    void Awake()
    {
        // Lấy thông tin từ ScriptableObject
        moveSpeed = enemyData.moveSpeed;
        currentHealth = enemyData.maxHealth;
        isRanged = enemyData.isRanged;
        attackCooldown = enemyData.attackCooldown;
        attackRange = enemyData.attackRange;
        bulletPrefab = enemyData.bulletPrefab;
        meleeDamage = enemyData.meleeDamage;
    }

    void Start()
    {
        if (!isRanged)
            SwitchState(meleeChaseState);
        else
            SwitchState(rangedApproachState);
    }

    void Update()
    {
        currentState?.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            SwitchState(deadState);
        }
    }
}
