using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingMenuManager : MonoBehaviour
{
    public TMP_Dropdown graphicsDrops;

    public void ChangeGraphicsQuality() {
        QualitySettings.SetQualityLevel(graphicsDrops.value);
    }
}
