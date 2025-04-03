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
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.Log("Đăng ký thất bại");
                return;
            }
            if (task.IsCompleted)
            {
                FirebaseUser newUser = task.Result.User;

                CharacterData player = ScriptableObject.CreateInstance<CharacterData>();
                player.Name = "Player2";
                player.CharacterDescription = "Xấu ncc";
                player.FullName = "Nguời chơi 2";

                FirebaseDatabase.DefaultInstance.RootReference
                    .Child("Users").Child(newUser.UserId).Child("Player")
                    .SetRawJsonValueAsync(JsonUtility.ToJson(player));

            }

            Debug.Log("Đăng ký thành công");
        });
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

            SceneManager.LoadScene("FirebaseData");
        });
    }
}
