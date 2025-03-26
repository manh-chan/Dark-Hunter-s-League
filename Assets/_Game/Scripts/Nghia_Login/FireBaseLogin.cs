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
    public InputField isReEmail;
    public InputField isRePassword;
    public Button btnRegister;

    [Header("Login")]
    public InputField isLoginEmail;
    public InputField isLoginPassword;
    public Button btnLogin;


    private FirebaseAuth auth;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        btnRegister.onClick.AddListener(RegisterFirebase);
        btnLogin.onClick.AddListener(LoginFirebase);
    }

    public void RegisterFirebase()
    {
        string email = isReEmail.text;
        string password = isRePassword.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Dk bi huy");
            }
            if(task.IsFaulted)
            {
                Debug.Log("Dk that bai");
            }
            if(task.IsCompleted)
            {
                Debug.Log("Dk thanh cong");
            }
        });
    }


    public void LoginFirebase()
    {
        string email = isReEmail.text;
        string password = isRePassword.text;

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Dn bi huy");
            }
            if (task.IsFaulted)
            {
                Debug.Log("Dn that bai");
            }
            if (task.IsCompleted)
            {
                Debug.Log("Dn thanh cong");
                FirebaseUser user = task.Result.User;

                //chuyen scene nao ghi ten scene do
                SceneManager.LoadScene("Menu");
            }
        });
    }
}
