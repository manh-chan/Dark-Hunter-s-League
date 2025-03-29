using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class CanvasHero : UICanvas
{
    public void MainMenuButton()
    {
        UIManager.Instance.OpenUI<CanvasMainMenu>();
       
    }
    public void BackButton()
    {
        Close(0);
       
    }
    public void StartButton()
    {
        SceneManager.LoadScene(Constant.SCENEN_LEVELSELECTER);
        Time.timeScale = 1.0f;
        
    }
}