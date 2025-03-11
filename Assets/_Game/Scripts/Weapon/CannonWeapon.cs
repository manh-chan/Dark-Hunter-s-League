using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonWeapon : Weapon
{
    protected float currentAttackInterval;
    protected int currentAttackCount;

    private float attackRange = 5f;
    private LayerMask enemyLayer;
    private Transform targetEnemy;
    private Cannon prefabCannon;
    
    protected override void Update()
    {
        base.Update();
        FindAndRotateToEnemy();
        if (currentAttackInterval > 0)
        {
            currentAttackInterval -= Time.deltaTime;
            if (currentAttackInterval <= 0)
            {
                Attack(currentAttackCount);
            }
        }
    }
    protected override void OnInit()
    {
        base.OnInit();
        enemyLayer = LayerMask.GetMask(Constant.LAYER_ENEMY);
        prefabCannon = Instantiate(currentStats.cannonPrefab, transform.position, Quaternion.identity);
    }
    public override bool CanAttack()
    {
        if (currentAttackCount > 0) return true;
        return base.CanAttack();
    }
    

    protected void FindAndRotateToEnemy()
    {
        targetEnemy = FindNearestEnemy();
        if (targetEnemy != null)
        {
            Vector3 direction = (targetEnemy.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Đảm bảo không bị lật ngược
            prefabCannon.transform.rotation = Quaternion.Euler(0, 0, angle);

            // Nếu bị lật, kiểm tra và điều chỉnh góc
            if (Mathf.Abs(angle) > 90)
            {
                prefabCannon.transform.localScale = new Vector3(1, -1, 1); // Lật dọc lại
            }
            else
            {
                prefabCannon.transform.localScale = new Vector3(1, 1, 1); // Giữ nguyên
            }
        }
    }

    protected Transform FindNearestEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(prefabCannon.transform.position, attackRange, enemyLayer);
        Transform nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            float distance = Vector2.Distance(prefabCannon.transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        return nearestEnemy;
    }
    protected override bool Attack(int attackCount = 1)
    {
        if (!currentStats.cannonPrefab)
        {
            Debug.LogWarning(string.Format("Projectile prefab has not been set for {0}", name));
            ActivateCooldown();
            return false;
        }

        if (!CanAttack()) return false;

        Projectile prefab = Instantiate(currentStats.projectilePrefab,
                    prefabCannon.transform.position,
                    prefabCannon.transform.rotation);

        prefab.weapon = this;
        prefab.owner = owner;

        ActivateCooldown();

        attackCount--;

        if (attackCount > 0)
        {
            currentAttackCount = attackCount;
            currentAttackInterval = ((WeaponData)data).baseStats.projectileInterval;
        }

        return true;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(prefabCannon.transform.position, attackRange);
    }
}
