using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ADev_loader : MonoBehaviour
{
    [SerializeField] private Image loadingImage;
    [SerializeField] private GameObject[] textLoadings;

    void Start()
    {
        StartCoroutine(LoadScenes());
    }

    IEnumerator LoadScenes()
    {
        loadingImage.fillAmount = 0;
        loadingImage.DOFillAmount(1, 3); // Thanh loading lấp đầy trong 3 giây
        yield return StartCoroutine(HandleTextLoading());
        SceneManager.LoadScene("MainStory");
       
    }

    IEnumerator HandleTextLoading()
    {
        float totalTime = 3f;
        float timeOnOff = 0.5f;

        for (float t = 0; t < totalTime; t += timeOnOff)
        {
            for (int i = 0; i < textLoadings.Length; i++)
            {
                textLoadings[i].SetActive(false);
            }

            textLoadings[(int)(t / timeOnOff) % textLoadings.Length].SetActive(true);

            yield return new WaitForSeconds(timeOnOff);
        }
    }
}
