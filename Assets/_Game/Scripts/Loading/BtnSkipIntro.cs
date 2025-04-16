using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnSkipIntro : MonoBehaviour
{
    public float introDuration = 10f; // Th?i gian t? ??ng chuy?n n?u kh�ng skip
    public string nextSceneName = "MainScene"; // T�n scene ch�nh
    public Button skipButton;

    private void Start()
    {
        // ?n n�t Skip l�c b?t ??u
        skipButton.gameObject.SetActive(false);

        // Hi?n n�t Skip sau 2 gi�y
        Invoke("ShowSkipButton", 2f);

        // G�n s? ki?n click v�o n�t Skip
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

