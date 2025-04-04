using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandDead : MonoBehaviour
{
    public float damageInterval = 0.5f; 
    public float damageAmount = 5f;
    public float damageRadius = 1.5f;


    public LayerMask targetLayer;
    private void Start()
    {
        StartCoroutine(DamagePlayerRoutine());
    }

    private IEnumerator DamagePlayerRoutine()
    {
        while (true)
        {
            LandorDead();
            yield return new WaitForSeconds(damageInterval);
        }
    }

    private void LandorDead()
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, damageRadius, targetLayer);
        if (hitCollider == null) return;
        PlayerStats character = hitCollider.GetComponent<PlayerStats>();
        if (character != null) character.TakeDamage(damageAmount);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius); 
    }
}
