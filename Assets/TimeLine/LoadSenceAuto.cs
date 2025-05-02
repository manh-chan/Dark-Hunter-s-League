using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class LoadSenceAuto : MonoBehaviour
{
    private float timeCount = 0;
    public int timeToSence;
    public int timeCountUser;
    public Button btnSkip;
    private SkipDataUser UserData;
    public bool checkUser;

    void Start()
    {
        btnSkip.gameObject.SetActive(false);
        string uid = PlayerPrefs.GetString("uid", "");
        UserFirebase.Instance.ReadUserProgressFromFirebase(uid, (loadedData) =>
        {
            if (loadedData == null)
            {
                // Nếu không có hoặc sai kích thước, tạo mới
                UserData = new SkipDataUser(true);
                UserFirebase.Instance.WritePlayerData(uid, UserData);
                checkUser = false;
            }
            else
            {
                UserData = loadedData;
                checkUser = true;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
        if (checkUser)
        {
            if (timeCount >= timeCountUser) btnSkip.gameObject.SetActive(true);
        }
        else
        {
            btnSkip.gameObject.SetActive(false);
        }
        if (timeCount >= timeToSence){
            SceneManager.LoadScene("LoadingG");
        } 
    }
    public void SkipButton() {
        SceneManager.LoadScene("LoadingG");
    }
}
