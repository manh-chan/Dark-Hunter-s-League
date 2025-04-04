using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonAttackEnemyMovement : EnemyMovement
{
    public float cooldownTime = 5f;
    public float distanceAttack = 3f;

    public GameObject summonPrefab; 

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
            StartCoroutine(Summon());
        }
    }

    private IEnumerator Summon()
    {
        isAttacking = true;

        yield return new WaitForSeconds(1f);

        GameObject summonedObject = Instantiate(summonPrefab, player.position, Quaternion.identity);

        cooldownTime = 5f;
        isAttacking = false;
        rezoSpeedMove = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceAttack);

    }
}
