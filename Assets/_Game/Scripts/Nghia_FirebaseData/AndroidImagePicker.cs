using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using TMPro;
using Firebase.Extensions;

public class AndroidImagePicker : MonoBehaviour
{
    public RawImage previewImage;          
    public TextMeshProUGUI nameInputText;  
    public TMP_InputField nameInputField;  

    private string base64AvatarString = ""; 


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

        string playerName = nameInputField.text;

        PlayerProfileData profileData = new PlayerProfileData
        {
            name = playerName,
            avatarBase64 = base64AvatarString
        };

        string json = JsonUtility.ToJson(profileData);

        Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference
            .Child("Users").Child(uid).Child("Profile") 
            .SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("Upload dữ liệu thành công cho UID: " + uid);
                }
                else
                {
                    Debug.LogError("Upload thất bại: " + task.Exception);
                }
            });
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
                    PlayerProfileData profileData = JsonUtility.FromJson<PlayerProfileData>(json);

                    nameInputText.text = profileData.name;

                    if (!string.IsNullOrEmpty(profileData.avatarBase64))
                    {
                        byte[] imageBytes = Convert.FromBase64String(profileData.avatarBase64);
                        Texture2D texture = new Texture2D(2, 2);
                        texture.LoadImage(imageBytes);
                        previewImage.texture = texture;
                    }

                    Debug.Log("Tải dữ liệu thành công cho UID: " + uid);
                }
                else
                {
                    Debug.LogWarning("Không tìm thấy dữ liệu profile cho UID: " + uid);
                }
            });
    }


    [Serializable]
    public class PlayerProfileData
    {
        public string name;
        public string avatarBase64;
    }
}
