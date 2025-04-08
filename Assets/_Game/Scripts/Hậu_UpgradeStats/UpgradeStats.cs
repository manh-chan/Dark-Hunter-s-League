using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class UpgradeStats
{
    public float maxHealthBonus;
    public float recoveryBonus;
    public float armorBonus;
    public float moveSpeedBonus;
    public float mightBonus;
    public float areaBonus;
    public float speedBonus;
    public float durationBonus;
    public float cooldownBonus;
    public float luckBonus;

    public UpgradeStats() { }
    public UpgradeStats(float maxHealthBonus, float recoveryBonus, float armorBonus, float moveSpeedBonus, float mightBonus, float areaBonus, float speedBonus, float durationBonus, float cooldownBonus, float luckBonus)
    {
        this.maxHealthBonus = maxHealthBonus;
        this.recoveryBonus = recoveryBonus;
        this.armorBonus = armorBonus;
        this.moveSpeedBonus = moveSpeedBonus;
        this.mightBonus = mightBonus;
        this.areaBonus = areaBonus;
        this.speedBonus = speedBonus;
        this.durationBonus = durationBonus;
        this.cooldownBonus = cooldownBonus;
        this.luckBonus = luckBonus;
    }
}
