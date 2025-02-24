using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHero : UICanvas
{
    public void MainMenuButton()
    {
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }
}