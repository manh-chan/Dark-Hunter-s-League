using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSenceAuto : MonoBehaviour
{
    private float timeCount = 0;
    public int timeToSence;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
        if (timeCount >= timeToSence) {
           
            SceneManager.LoadScene("LoadingG");
        } 
    }
}
