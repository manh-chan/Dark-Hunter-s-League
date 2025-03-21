using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHero : UICanvas
{
    public void MapButton()
    {
        UIManager.Instance.OpenUI<CanvasMap>();
    }
    public void MainMenuButton()
    {
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }
}