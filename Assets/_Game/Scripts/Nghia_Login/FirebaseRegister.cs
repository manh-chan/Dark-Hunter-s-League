using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirebaseRegister : MonoBehaviour
{
    [Header("Register")]
    public InputField isReEmail;
    public InputField isRePassword;
    public Button btnRegister;
    public Button btnLogin;

    private FirebaseAuth auth;
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        btnRegister.onClick.AddListener(RegisterFirebase);
        btnLogin.onClick.AddListener(LoadLogin);
    }

    public void RegisterFirebase()
    {
        string email = isReEmail.text;
        string password = isRePassword.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.Log("Đăng ký thất bại");
                return;
            }
            if (task.IsCompleted)
            {
                FirebaseUser newUser = task.Result.User;

                UpgradeStats player = new UpgradeStats();

                FirebaseDatabase.DefaultInstance.RootReference
                    .Child("Users").Child(newUser.UserId).Child("Player")
                    .SetRawJsonValueAsync(JsonUtility.ToJson(player));

            }

            SceneManager.LoadScene("Login");
            Debug.Log("Đăng ký thành công");
        });
    }

    public void LoadLogin()
    {
        SceneManager.LoadScene("Login");
    }

}
