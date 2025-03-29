using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class BlackShield : EnemyMovement
{
    public float cooldownTime = 5f;
    public float distanceAttack = 0.5f;

    public Animator ani;
    public bool isAttacking = false;
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
            StartCoroutine(Shield());
        }
    }

    private IEnumerator Shield()
    {
        isAttacking = true;
        ani.enabled = false;
        transform.localScale = Vector3.one;
        yield return new WaitForSeconds(5f);

        ani.enabled = true;
        cooldownTime = 5f;
        ani.gameObject.SetActive(true);
        isAttacking = false;
        rezoSpeedMove = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceAttack);

    }
}
