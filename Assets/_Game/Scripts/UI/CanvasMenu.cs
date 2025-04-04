using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasMenu : UICanvas
{
    public void PlayButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>();
    }

    public void SettingsButton()
    {
        UIManager.Instance.OpenUI<CanvasSettings>().SetState(this);
    }

    public void BaseButton()
    {
        UIManager.Instance.OpenUI<CanvasBase>();
    }

    public void GrowthButton()
    {
        UIManager.Instance.OpenUI<CanvasGrowth>();
    }

    public void HeroButton()
    {
        UIManager.Instance.OpenUI<CanvasHero>();
    }
}
