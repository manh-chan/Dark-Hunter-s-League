using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BlackSniper : EnemyMovement
{
    public float cooldownTime = 0f;
    public float distanceAttack = 0.5f;
    public float waitForTimeAttack = 1.5f;
    public Transform initialPosition;

    public GameObject buletBlackSniper;
    public Animator ani;
    public bool isAttacking = false;

    private Color color1 = Color.white;
    private Color color2 = Color.red;
    private float duration = 0.2f;

    private float distance;
    private float timeCheck = 3;

    private Coroutine colorCoroutine;
    private PlayerStats playerStats;
    protected override void Start()
    {
        base.Start();
        playerStats = player.GetComponent<PlayerStats>();
    }

    protected override void Update()
    {
        base.Update();
        cooldownTime -= Time.deltaTime;
        timeCheck -= Time.deltaTime;

        distance = Vector3.Distance(transform.position, player.position);

        if (cooldownTime <= 0 && !isAttacking && distance <= distanceAttack)
        {
            pushForce = 0;
            rezoSpeedMove = false;
            StartCoroutine(Sniper());
        }
        CheckTarget();
    }
    private IEnumerator Sniper()
    {
        isAttacking = true;
        
        ani.enabled = false;
        colorCoroutine = StartCoroutine(ChangeColorLoop());
        yield return new WaitForSeconds(waitForTimeAttack);

        Instantiate(buletBlackSniper, initialPosition.position,Quaternion.identity);

        if (colorCoroutine != null)
        {
            StopCoroutine(colorCoroutine);
            colorCoroutine = null;
            sorted.color = color1; 
        }
        ani.enabled = true;
        cooldownTime = 5f;
        pushForce = 1;
        isAttacking = false;
        if(distance >= distanceAttack) rezoSpeedMove = true;
    }
    private void CheckTarget()
    {
        if(timeCheck <= 0)
        {
            if(distance >= distanceAttack)
            {
                rezoSpeedMove = true;
                if (playerStats != null)
                {
                    playerStats.DisableEffect();
                }
            }
            if (distance <= distanceAttack)
            {
                rezoSpeedMove = false;
                if (playerStats != null)
                {
                    playerStats.EnableEffect();
                }
            }

            timeCheck = 0.5f;
        }
    }
    void OnDestroy()
    {
        playerStats.DisableEffect();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceAttack);

    }
    IEnumerator ChangeColorLoop()
    {
        while (true)
        {
            sorted.color = color2;
            yield return new WaitForSeconds(duration);
            sorted.color = color1;
            yield return new WaitForSeconds(duration);
        }
    }
}
