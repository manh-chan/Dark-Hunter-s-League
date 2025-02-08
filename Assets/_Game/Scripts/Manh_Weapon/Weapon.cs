using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Weapon : GameUnit
{
    public LayerMask enemyLayer;  // Lớp kẻ địch
    public Collider2D hitCollider;  // Collider để gây sát thương
    public bool isAttacking = false;

    protected Vector3 initialPosition;
    protected Transform nearestEnemy;
    [System.Serializable]
    public class weaponStarts { 
        
    }
    private void Start()
    {
        OnInit();
    }
    private void Update()
    {
        Rotate();
        Attack();
    }
    protected virtual void OnInit() 
    { 
        
    }
    protected virtual void Attack() { 
    
    }
    protected virtual void Rotate() 
    {
        Transform nearestEnemy = FindNearestEnemy(10f);
        if (nearestEnemy != null)
        {
            Vector3 direction = (nearestEnemy.position - transform.position).normalized;
            // Xoay giáo theo hướng enemy
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    protected Transform FindNearestEnemy(float attackRange)
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        Transform nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        return nearestEnemy;
    }
   
}
