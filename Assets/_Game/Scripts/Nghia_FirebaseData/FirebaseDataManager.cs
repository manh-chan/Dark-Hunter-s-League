using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseDataManager : Singleton<FirebaseDataManager>
{
    public UpgradeStats player;

    public TextMeshProUGUI showReadPlayerData;
    public TextMeshProUGUI showWritePlayerData;

    private DatabaseReference reference;

    private void Awake()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    void Start()
    {
        
        string uid = PlayerPrefs.GetString("uid", "");
        if (!string.IsNullOrEmpty(uid))
        {
            ReadPlayerData(uid);
        }
        else
        {
            ShowReadPlayerData("Chưa có UID, vui lòng đăng nhập trc.");
        }
    }

    public void WritePlayerData(string id, UpgradeStats player)
    {
        string json = JsonUtility.ToJson(player);

        reference.Child("Users").Child(id).Child("Player").SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                ShowWritePlayerData("Ghi dữ liệu người chơi thành công");
            }
            else
            {
                ShowWritePlayerData("Ghi dữ liệu người chơi thất bại: " + task.Exception);
            }
        });
    }

    public void ReadPlayerData(string id)
    {
        reference.Child("Users").Child(id).Child("Player").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    // Đọc dữ liệu từ Firebase
                    player = JsonUtility.FromJson<UpgradeStats>(snapshot.GetRawJsonValue());

                    // Cập nhật dữ liệu vào UI ngay khi đọc thành công
                    UpgradeUI ui = FindObjectOfType<UpgradeUI>();
                    if (ui != null)
                    {
                        ui.upgradeStats = player; // Cập nhật upgradeStats
                        ui.UpdateUI(); // Cập nhật UI ngay lập tức
                    }

                    Debug.Log("Đọc dữ liệu người chơi thành công.");
                   
                }
              
            }
          
        });
    }



    public void ShowReadPlayerData(string message)
    {
        showReadPlayerData.text = message;
    }

    public void ShowWritePlayerData(string message)
    {
        showWritePlayerData.text = message;
    }
}
