using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : UILevelSelector
{

    public override void Awake()
    {
        base.Awake();
        //int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i <currentMap; i++)
        {
            Debug.Log("i");
        }
        //for (int i = 0; i < unlockedLevel; i++)
        //{
        //    buttons[i].interactable = true;
        //}
    }
    private void Start()
    {
        Debug.Log(currentMap);
        for (int i = 0; i < currentMap; i++)
        {
            Debug.Log("i");
        }
    }
    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

}
    
