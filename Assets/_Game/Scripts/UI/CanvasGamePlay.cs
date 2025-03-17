using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] TextMeshProUGUI coinText;

    public override void Setup()
    {
        base.Setup();
        UpdateCoin(0);
    }
    //xu
    public void UpdateCoin(int coin)
    {
        coinText.text = coin.ToString();
    }

    public void SettingsButton()
    {
        UIManager.Instance.OpenUI<CanvasSettings>().SetState(this);
    }
}
