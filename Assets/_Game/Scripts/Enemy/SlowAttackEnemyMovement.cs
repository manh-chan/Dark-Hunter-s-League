using System.Collections;
using UnityEngine;

public class SlowAttackEnemyMovement : EnemyMovement
{
    public float cooldownTime = 5f;
    public float distanceAttack = 3f; 
    public float moveSpeedAttack = 5f; 
    public float maxDistance = 3f; 

    private Vector2 startPosition;
    private Vector2 chargeDirection;
    private bool isAttacking = false;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        cooldownTime -= Time.deltaTime;
    }

    public override void Move()
    {
        base.Move();
        float distance = Vector3.Distance(transform.position, player.position);

        if (cooldownTime <= 0 && !isAttacking && distance <= distanceAttack)
        {
            rezoSpeedMove = false;
            StartCoroutine(AttackSequence());
        }
    }

    private IEnumerator AttackSequence()
    {
        isAttacking = true;
        startPosition = transform.position; 

        float approachTime = 0.5f;
        float elapsed = 0f;
        while (elapsed < approachTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        chargeDirection = (player.position - transform.position).normalized;
        float traveledDistance = 0f;

        while (traveledDistance < maxDistance)
        {
            float step = moveSpeedAttack * Time.deltaTime;
            transform.position += (Vector3)chargeDirection * step;
            traveledDistance += step;
            yield return null;
        }

        cooldownTime = 5f;
        isAttacking = false;
        rezoSpeedMove = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceAttack);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}
