using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class UIWeapon : MonoBehaviour
{
    public static WeaponData selected;

    [Header("Template")]
    public Toggle toggleTemplate;
    public string weaponIconPath = "Weapon Icon";
    public List<Toggle> selectableToggles = new List<Toggle>();

    [Header("DescriptionBox")]
    public TMP_Text weaponFullName;
    public TMP_Text weaponDescription;
    public Image selectedweaponIcon;

    public static WeaponData[] GetAllWeaponDataAssets()
    {
        List<WeaponData> weapons = new List<WeaponData>();
#if UNITY_EDITOR
        string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
        foreach (string assetPath in allAssetPaths)
        {
            if (assetPath.EndsWith(".asset"))
            {
                WeaponData weaponsData = AssetDatabase.LoadAssetAtPath<WeaponData>(assetPath);
                if (weaponsData != null) weapons.Add(weaponsData);
            }
        }
#else
        Debug.LogWarning("This function cannot bo called on builds");
#endif
        return weapons.ToArray();
    }
    public static WeaponData GetData()
    {
        if (selected)
        {
            return selected;
        }
        else
        {
            WeaponData[] weapons = GetAllWeaponDataAssets();
            if (weapons.Length > 0) return weapons[Random.Range(0, weapons.Length)];
        }
        return null;
    }
    public void Select(WeaponData weapon)
    {
        weaponFullName.text = weapon.name;
        weaponDescription.text = weapon.baseStats.description;
        selectedweaponIcon.sprite = weapon.icon;
    }
}
