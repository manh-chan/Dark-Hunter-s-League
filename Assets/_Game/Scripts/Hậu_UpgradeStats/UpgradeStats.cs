using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "UpgradeStats", menuName = "Dark-Hunter-s-League/UpgradeStats")]
public class UpgradeStats : ScriptableObject
{
    public float maxHealthBonus = 0;
    public float recoveryBonus = 0;
    public float armorBonus = 0;
    public float moveSpeedBonus = 0;
    public float mightBonus = 0;
    public float areaBonus = 0;
    public float speedBonus = 0;
    public float durationBonus = 0;
    public int amountBonus = 0; // không thể cộng
    public float cooldownBonus = 0;
    public float luckBonus = 0;

}
