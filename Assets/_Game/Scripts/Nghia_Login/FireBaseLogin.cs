using Firebase.Auth;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBaseLogin : MonoBehaviour
{
    public InputField isEmail;
    public InputField isPassword;

    public Button btnLogin;
    public Button btnRegister;

    private FirebaseAuth auth;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        btnRegister.onClick.AddListener(RegisterFirebase);
    }

    public void RegisterFirebase()
    {
        string email = isEmail.text;
        string password = isPassword.text;

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
}
