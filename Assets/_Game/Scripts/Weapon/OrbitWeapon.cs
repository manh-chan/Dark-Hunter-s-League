using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitWeapon : Weapon
{
    protected float currentAttackInterval;
    protected int currentAttackCount;
    private List<OrbitProjectile> activeProjectiles = new List<OrbitProjectile>();

    protected override void Update()
    {
        base.Update();
        activeProjectiles.RemoveAll(proj => proj == null); // Xóa hạt đã biến mất

        if (currentAttackInterval > 0)
        {
            currentAttackInterval -= Time.deltaTime;
            if (currentAttackInterval <= 0 && activeProjectiles.Count == 0) // Chờ hết hạt rồi mới bắn tiếp
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
        if (!currentStats.OrbitPrefab)
        {
            Debug.LogWarning("Orbit projectile prefab chưa được gán!");
            ActivateCooldown();
            return false;
        }

        if (!CanAttack()) return false;


        for (int i = 0; i < attackCount; i++)
        {
            float angleStep = 360f / attackCount;
            float startAngle = i * angleStep;

            OrbitProjectile orb = Instantiate(currentStats.OrbitPrefab, owner.transform.position, Quaternion.identity);
            orb.weapon = this;
            orb.owner = owner;
            orb.startAngle = startAngle;

            activeProjectiles.Add(orb);
        }

        ActivateCooldown();

        return true;
    }

    public void StartRetraction()
    {
        foreach (var orb in activeProjectiles)
        {
            if (orb != null)
            {
                orb.SetRetract();
            }
        }
    }
}
