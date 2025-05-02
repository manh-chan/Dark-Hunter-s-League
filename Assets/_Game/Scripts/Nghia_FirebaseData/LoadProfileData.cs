using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Firebase.Extensions;
using System;

public class LoadProfileData : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public RawImage previewImage;


    private void Start()
    {
        LoadFromFirebase();
    }
    public void LoadFromFirebase()
    {
        string uid = PlayerPrefs.GetString("uid", "");

        if (string.IsNullOrEmpty(uid))
        {
            Debug.LogWarning("Không có UID, chưa đăng nhập!");
            return;
        }

        Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference
            .Child("Users").Child(uid).Child("Profile")
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted && task.Result.Exists)
                {
                    string json = task.Result.GetRawJsonValue();
                    UserProfileData profileData = JsonUtility.FromJson<UserProfileData>(json);

                    nameText.text = profileData.name;

                    if (!string.IsNullOrEmpty(profileData.avatarBase64))
                    {
                        byte[] imageBytes = Convert.FromBase64String(profileData.avatarBase64);
                        Texture2D texture = new Texture2D(2, 2);
                        texture.LoadImage(imageBytes);
                        previewImage.texture = texture;
                    }

                    Debug.Log("Tải dữ liệu thành công");
                }
                else
                {
                    Debug.LogWarning("Không tìm thấy dữ liệu");
                }
            });
    }

    
}
