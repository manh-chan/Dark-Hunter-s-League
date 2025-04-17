using UnityEngine;
using TMPro;
using Firebase.Extensions;
using System;
using System.IO;
using UnityEngine.UI;

public class UploadProfileData : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public RawImage previewImage;

    private string base64AvatarString = "";
    //button chon anh
    public void PickImage()
    {
        NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                byte[] imageBytes = File.ReadAllBytes(path);
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(imageBytes);
                previewImage.texture = texture;

                base64AvatarString = Convert.ToBase64String(imageBytes);
            }
        }, "Chọn ảnh đại diện", "image/*");
    }
  
    public void UploadToFirebase()
    {
        string uid = PlayerPrefs.GetString("uid", "");

        if (string.IsNullOrEmpty(uid))
        {
            Debug.LogWarning("Không có UID, chưa đăng nhập!");
            return;
        }

        UserProfileData profileData = new UserProfileData
        {
            name = nameInputField.text,
            avatarBase64 = base64AvatarString
        };

        string json = JsonUtility.ToJson(profileData);

        Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference
            .Child("Users").Child(uid).Child("Profile")
            .SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Upload dữ liệu thành công");
                }
                else
                {
                    Debug.LogError("Upload thất bại: " + task.Exception);
                }
            });
    }
}
