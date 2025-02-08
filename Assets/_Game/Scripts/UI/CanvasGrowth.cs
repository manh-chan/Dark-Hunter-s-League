using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGrowth : UICanvas
{
    public void MainMenuButton()
    {
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }
}
