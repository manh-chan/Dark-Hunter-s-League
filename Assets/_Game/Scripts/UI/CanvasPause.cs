using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPause : UICanvas
{
    public void SettingsButton()
    {
        UIManager.Instance.OpenUI<CanvasSettings>().SetState(this);
    }
}
