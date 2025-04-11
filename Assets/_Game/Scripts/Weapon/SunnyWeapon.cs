using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunnyWeapon : Weapon
{
    //[SerializeField] private GameObject projectilePrefab;
    //[SerializeField] private Transform target;
    //[SerializeField] private AnimationCurve trajectoryAnimationCurve;
    //[SerializeField] private AnimationCurve axisCorrectionAnimtionCurve;
    //[SerializeField] private AnimationCurve projectileSpeedAnimtionCurve;

    //[SerializeField] private float shootRate;
    //[SerializeField] private float projectileMaxMoveSpeed;
    //[SerializeField] private float projectileRelativeHeight;
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
    public override bool CanAttack()
    {
        if (currentAttackCount > 0) return true;
        return base.CanAttack();
    }
    protected override bool Attack(int attackCount = 1)
    {
        if (!currentStats.projectilePrefab)
        {
            Debug.LogWarning(string.Format("Projectile prefab has not been set for {0}", name));
            ActivateCooldown();
            return false;
        }

        if (!CanAttack()) return false;

        float spawnAngle = GetSpawnAngle();

        Projectile prefab = Instantiate(currentStats.projectilePrefab,
            owner.transform.position + (Vector3)GetSpawnOffset(spawnAngle),
            Quaternion.Euler(0, 0, spawnAngle));

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
    protected virtual float GetSpawnAngle()
    {
        return Mathf.Atan2(playerMovement.LastMovedVector.y, playerMovement.LastMovedVector.x) * Mathf.Rad2Deg;
    }
    protected virtual Vector2 GetSpawnOffset(float spawnAngle = 0)
    {
        return Quaternion.Euler(0, 0, spawnAngle) * new Vector2(
            Random.Range(currentStats.spawnVariace.xMin, currentStats.spawnVariace.xMax),
            Random.Range(currentStats.spawnVariace.yMin, currentStats.spawnVariace.yMax));
    }
}
