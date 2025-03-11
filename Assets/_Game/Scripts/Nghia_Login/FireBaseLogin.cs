using Firebase.Auth;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FireBaseLogin : MonoBehaviour
{
    [Header("Register")]
    public InputField isRegiEmail;
    public InputField isRegiPass;
    public Button btnRegis;

    [Header("Login")]
    public InputField isLoginEmail;
    public InputField isLoginPass;
    public Button btnLogin;

    private FirebaseAuth auth;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        btnRegis.onClick.AddListener(RegisAccFire);
        btnLogin.onClick.AddListener(LoginAccFire);
    }

    public void RegisAccFire()
    {
        string email = isRegiEmail.text;
        string pass = isRegiPass.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, pass).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Dk bi huy");
                return;
            }

            if (task.IsFaulted)
            {
                Debug.Log("Dk that bai");
                return;
            }

            if (task.IsCompleted)
            {
                Debug.Log("Dk thanh cong");
                return;
            }
        });
    }

    public void LoginAccFire()
    {
        string email = isLoginEmail.text;
        string pass = isLoginPass.text;

        auth.SignInWithEmailAndPasswordAsync(email, pass).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("DN bi huy");
                return;
            }

            if (task.IsFaulted)
            {
                Debug.Log("DN that bai");
                return;
            }

            if (task.IsCompleted)
            {
                Debug.Log("DN thanh cong");
                FirebaseUser user = task.Result.User;

                //muốn đăng nhập vào scene nào thì đổi tên scene đấy

                SceneManager.LoadScene("Play");
                return;
            }
        });
    }
}
