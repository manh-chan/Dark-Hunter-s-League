using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAchievement : UICanvas
{
    public void MainMenuButton()
    {
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }
}
