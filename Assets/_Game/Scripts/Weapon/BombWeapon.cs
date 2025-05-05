using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWeapon : Weapon
{
    protected float currentAttackInterval;
    protected int currentAttackCount;

    protected override void Update()
    {
        base.Update();

        if (currentAttackInterval > 0)
        {
            currentAttackInterval -= Time.deltaTime;
            if (currentAttackInterval <= 0 ) 
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
        if (playerMovement.movIng == false) return false;

        if (!currentStats.bombPrefab)
        {
            Debug.LogWarning("Orbit projectile prefab chưa được gán!");
            ActivateCooldown();
            return false;
        }

        if (!CanAttack()) return false;

        Bomb prefab = Instantiate(currentStats.bombPrefab, owner.transform.position, Quaternion.identity);
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
}
