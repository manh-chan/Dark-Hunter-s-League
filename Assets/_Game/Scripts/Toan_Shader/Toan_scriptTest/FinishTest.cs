using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UnlockNewLevel();
            SceneControllerTest.instance.NextLevel();
        }
    }
    void UnlockNewLevel()
    {                                                                                       
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex")) //lấy số index của scene hiện tại so sánh với số map lớn nhất đã chơi 
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1); //set lại số màn lớn nhất đã chơi thành scene hiện tại + 1
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);//set lại data
            PlayerPrefs.Save();
        }
    }

}
