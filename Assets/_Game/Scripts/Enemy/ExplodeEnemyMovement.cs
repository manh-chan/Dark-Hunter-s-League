using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ExplodeEnemyMovement : EnemyMovement
{
    public float distanceAttack = 0.5f;
    public float damageRadius = 3f;  
    public float damageAmount = 20f;

    public LayerMask targetLayer;
    public ParticleSystem ParticleSystem;

    private Color color1 = Color.white;
    private Color color2 = Color.red;
    private float duration = 0.1f;

    protected override void Start()
    {
        base.Start();
    }

    public override void Move()
    {
        base.Move();
        float distance = Vector3.Distance(transform.position, player.position);
        
        if (distance <= distanceAttack)
        {
            pushForce = 0;
            rezoSpeedMove = false;
            StartCoroutine(ChangeColorLoop());
            Invoke(nameof(ExplodeEnemy), 1f);
        }
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
    private void ExplodeEnemy()
    {
        DealDamage();
        Instantiate(ParticleSystem, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    public void DealDamage()
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, damageRadius, targetLayer);
        if (hitCollider == null) return;
        PlayerStats character = hitCollider.GetComponent<PlayerStats>();
        if (character != null) character.TakeDamage(damageAmount);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawWireSphere(transform.position, distanceAttack);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
