using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Projectile;

public class OrbitProjectile : WeaponEffect
{
    protected int piercing;

    public float orbitSpeed = 360f;
    public float maxRadius = 1f;
    public float expansionSpeed = 2f;
    public int maxRotations = 2;

    private float currentRadius = 0f;
    private float angle = 0f;
    private int completedRotations = 0;
    private Dictionary<EnemyState, float> damageTimers = new Dictionary<EnemyState, float>();

    public float startAngle;
    private bool isRetracting = false;

    private void Start()
    {
        angle = startAngle; 
    }

    private void Update()
    {
        if (owner == null) return;

        angle += orbitSpeed * Time.deltaTime;
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * currentRadius;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * currentRadius;
        transform.position = owner.transform.position + new Vector3(x, y, 0);

        if (angle >= 360f)
        {
            angle -= 360f;
            completedRotations++;

            if (completedRotations >= maxRotations)
            {
                if (weapon is OrbitWeapon orbitWeapon)
                {
                    orbitWeapon.StartRetraction(); 
                }
            }
        }

        if (!isRetracting)
        {
            currentRadius += expansionSpeed * Time.deltaTime;
            if (currentRadius >= maxRadius) currentRadius = maxRadius;
        }
        else
        {
            currentRadius -= expansionSpeed * Time.deltaTime;
            if (currentRadius <= 0)
            {
                Destroy(gameObject);
            }
        }

    }

    public void SetRetract()
    {
        isRetracting = true;
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        EnemyState es = other.GetComponent<EnemyState>();
        if(es == null) return;
        es.TakeDamage(GetDamage());

        Weapon.Stats stats = weapon.GetStats();
        weapon.ApplyBuffs(es);
        piercing--;
        if (stats.hitEffect)
        {
            Destroy(Instantiate(stats.hitEffect, transform.position, Quaternion.identity), 5f);
        }
        if (piercing <= 0)
        {
            Destroy(gameObject);
        }
    }
}
