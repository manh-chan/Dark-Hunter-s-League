using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public InputField confirmPassword;
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

        string confirm = confirmPassword.text;

       
        if (password != confirm)
        {
            Debug.LogWarning("Mật khẩu và xác nhận mật khẩu không khớp!");
            // Nếu có UI thông báo thì show ở đây nữa
            return;
        }


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
                string userId = newUser.UserId;

                UpgradeStats player = new UpgradeStats();

                DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
                reference.Child("Users").Child(userId).Child("Player")
                    .SetRawJsonValueAsync(JsonUtility.ToJson(player));

                // Lưu email và mật khẩu để dùng cho chức năng quên mật khẩu
                reference.Child("AccountInfos").Child(email.Replace(".", "_")).Child("Password")
                    .SetValueAsync(password);
            }

            Debug.Log("Đăng ký thành công");
            SceneManager.LoadScene("Login");
        });
    }

    public void LoadLogin()
    {
        SceneManager.LoadScene("Login");
    }

}
