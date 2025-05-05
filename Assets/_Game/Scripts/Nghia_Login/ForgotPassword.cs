using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ForgotPassword : MonoBehaviour
{

    public TMP_InputField emailInput;
    public Button resetPasswordButton;
    public TextMeshProUGUI messageText;

    private FirebaseAuth auth;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        resetPasswordButton.onClick.AddListener(SendResetEmail);
    }

    void SendResetEmail()
    {
        string email = emailInput.text;

        if (string.IsNullOrEmpty(email))
        {
            messageText.text = "Vui lòng nhập email.";
            return;
        }

        auth.SendPasswordResetEmailAsync(email).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                var baseEx = task.Exception?.GetBaseException();
                if (baseEx != null && baseEx.Message.Contains("no user record"))
                {
                    messageText.text = "Email không tồn tại trong hệ thống.";
                }
                else
                {
                    messageText.text = "Gửi email thất bại. Kiểm tra lại!";
                }

                return;
            }

            messageText.text = "Đã gửi email khôi phục mật khẩu!";
        });
    }
}
