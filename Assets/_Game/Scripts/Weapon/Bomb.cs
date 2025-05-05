using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : WeaponEffect
{
    public GameObject explosionPrefab; // Prefab vụ nổ sẽ gọi khi phát nổ
    public float explosionRadius = 0.3f; // Bán kính gây sát thương

    private void Start()
    {
        Weapon.Stats stats = weapon.GetStats();

        float area = weapon.GetArea();
        if (area <= 0) area = 1;
        transform.localScale = new Vector3(
            area * Mathf.Sign(transform.localScale.x),
            area * Mathf.Sign(transform.localScale.y), 1);

        explosionRadius = area * 1f - 0.7f;
        explosionRadius = Mathf.Max(0.1f, explosionRadius); 

        Invoke(nameof(Explode), 1.5f);
    }

    private void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in hits)
        {
            EnemyState es = hit.GetComponent<EnemyState>();
            if (es != null)
            {
                es.TakeDamage(GetDamage());
                weapon.ApplyBuffs(es);

                Weapon.Stats stats = weapon.GetStats();
                if (stats.hitEffect)
                {
                    Destroy(Instantiate(stats.hitEffect, es.transform.position, Quaternion.identity), 5f);
                }
            }
        }

        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            float area = weapon.GetArea();
            if (area <= 0) area = 1;
            explosion.transform.localScale = new Vector3(
                area * Mathf.Sign(transform.localScale.x),
                area * Mathf.Sign(transform.localScale.y),
                1f
            );

            Destroy(explosion, 0.3f);
        }

        Destroy(gameObject); 
    }


    private void Update()
    {
        if (owner == null) return;
    }

}
