using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnSkipIntro : MonoBehaviour
{
    public float introDuration = 10f; // Th?i gian t? ??ng chuy?n n?u không skip
    public string nextSceneName = "MainScene"; // Tên scene chính
    public Button skipButton;

    private void Start()
    {
        // ?n nút Skip lúc b?t ??u
        skipButton.gameObject.SetActive(false);

        // Hi?n nút Skip sau 2 giây
        Invoke("ShowSkipButton", 2f);

        // Gán s? ki?n click vào nút Skip
        skipButton.onClick.AddListener(SkipIntro);

        // T? ??ng chuy?n c?nh sau m?t kho?ng th?i gian
        Invoke("SkipIntro", introDuration);
    }

    void ShowSkipButton()
    {
        skipButton.gameObject.SetActive(true);
    }

    public void SkipIntro()
    {
        SceneManager.LoadScene("Menu");
    }
}

