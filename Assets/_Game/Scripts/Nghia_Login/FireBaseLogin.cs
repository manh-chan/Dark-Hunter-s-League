using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FireBaseLogin : MonoBehaviour
{


    [Header("Login")]
    public InputField isLoginEmail;
    public InputField isLoginPassword;
    public Button btnLogin;
    public Button btnRegister;

    private FirebaseAuth auth;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        btnLogin.onClick.AddListener(LoginFirebase);
        btnLogin.onClick.AddListener(LoadRegister);
    }



    public void LoginFirebase()
    {
        string email = isLoginEmail.text;
        string password = isLoginPassword.text;

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.Log("Đăng nhập thất bại");
                return;
            }

            Debug.Log("Đăng nhập thành công");

            FirebaseUser user = task.Result.User;

            // Load scene và lưu UID vào PlayerPrefs (tạm thời)
            PlayerPrefs.SetString("uid", user.UserId);
            PlayerPrefs.Save();

            SceneManager.LoadScene("Menu");
        });
    }

    public void LoadRegister()
    {
        SceneManager.LoadScene("Register");
    }
}
