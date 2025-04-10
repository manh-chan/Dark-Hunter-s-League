using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] bool goNextLevel;
    [SerializeField] string levelName;
    public void OpenLevel(int levelId)
    {
        string levelName = "ToanMAP " + levelId;
        SceneManager.LoadScene(levelName);
    }
}
