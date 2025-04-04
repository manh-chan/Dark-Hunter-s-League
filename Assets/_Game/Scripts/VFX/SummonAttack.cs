using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonAttack : MonoBehaviour
{
    public float damageRadius = 3f;
    public float damageAmount = 20f;
    public float timeAttack = 1;
    public LayerMask targetLayer;
    public GameObject Explode;
    void Start()
    {
        Invoke(nameof(SumomExplode), timeAttack);
        Invoke(nameof(SummonAttacks), timeAttack);
        Invoke(nameof(Dest), timeAttack + 0.2f);
    }
    private void SumomExplode()
    {
        Instantiate(Explode,transform.position,Quaternion.identity);
    }
    private void SummonAttacks()
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, damageRadius, targetLayer);
        if (hitCollider == null) return;
        PlayerStats character = hitCollider.GetComponent<PlayerStats>();
        if (character != null) character.TakeDamage(damageAmount);
        
    }
    private void Dest()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
