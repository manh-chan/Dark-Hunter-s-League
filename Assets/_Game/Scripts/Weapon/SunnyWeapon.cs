/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunnyWeapon : Weapon
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform target;
    //[SerializeField] private AnimationCurve trajectoryAnimationCurve;
    //[SerializeField] private AnimationCurve axisCorrectionAnimtionCurve;
    //[SerializeField] private AnimationCurve projectileSpeedAnimtionCurve;

    [SerializeField] private float projectileMaxMoveSpeed = 1f;
    [SerializeField] private float projectileRelativeHeight = 2f;
    [SerializeField] private float spacing = 1f;
    [SerializeField] private float attackRange = 5f;

    [SerializeField] private LayerMask enemyLayer;
    //private float shootTimer;

    //private void Update()
    //{
    //    shootTimer -= Time.deltaTime;
    //    if (shootTimer <= 0)
    //    {
    //        shootTimer = shootRate;

    //        float spacing = 1f; // Khoảng cách giữa các viên đạn theo trục Y
    //        for (int i = -1; i <= 1; i++)
    //        {
    //            Vector3 spawnPosition = transform.position + new Vector3(0, i * spacing, 0); // Điều chỉnh theo trục Y
    //            Projectile1 projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity).GetComponent<Projectile1>();

    //            projectile.InitializeProjectile(target, projectileMaxMoveSpeed, projectileRelativeHeight);
    //            projectile.InitializeAnimationCurve(trajectoryAnimationCurve, axisCorrectionAnimtionCurve, projectileSpeedAnimtionCurve);
    //        }
    //    }
    //}
    protected float currentAttackInterval;
    protected int currentAttackCount;
    
    protected override void Update()
    {
        base.Update();

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
    }
    public override bool CanAttack()
    {
        if (currentAttackCount > 0) return true;
        return base.CanAttack();
    }
    protected override bool Attack(int attackCount = 1)
    {
        if (!currentStats.SunnyPrefab)
        {
            Debug.LogWarning(string.Format("Projectile prefab has not been set for {0}", name));
            ActivateCooldown();
            return false;
        }

        if (!CanAttack()) return false;

        //Projectile prefab = Instantiate(currentStats.projectilePrefab,
        //    owner.transform.position + (Vector3)GetSpawnOffset(spawnAngle),
        //    Quaternion.Euler(0, 0, spawnAngle));
        for (int i = -1; i <= 1; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, i * spacing, 0); // Điều chỉnh theo trục Y
            ProjectileSunny projectile = Instantiate(currentStats.SunnyPrefab, spawnPosition, Quaternion.identity).GetComponent<ProjectileSunny>();

            projectile.InitializeProjectile(FindNearestEnemy(), projectileMaxMoveSpeed, projectileRelativeHeight);
            projectile.InitializeAnimationCurve(currentStats.trajectoryAnimationCurve, currentStats.axisCorrectionAnimtionCurve, currentStats.projectileSpeedAnimtionCurve);
        }

        //prefab.weapon = this;
        //prefab.owner = owner;

        ActivateCooldown();

        attackCount--;

        if (attackCount > 0)
        {
            currentAttackCount = attackCount;
            currentAttackInterval = ((WeaponData)data).baseStats.projectileInterval;
        }

        return true;
    }
    protected Transform FindNearestEnemy()
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
*/