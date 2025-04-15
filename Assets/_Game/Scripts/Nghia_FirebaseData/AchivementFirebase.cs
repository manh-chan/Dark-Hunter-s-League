using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchivementFirebase : Singleton<AchivementFirebase>
{
    public Achivement achivement;

    private DatabaseReference reference;

    private void Awake()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    void Start()
    {

        string uid = PlayerPrefs.GetString("uid", "");
        Debug.Log(uid);
        if (!string.IsNullOrEmpty(uid))
        {
            ReadAcData(uid);
        }
        else
        {
            Debug.Log("Chưa có UID, vui lòng đăng nhập trc.");
        }
    }

    public void WritePlayerData(string id, Achivement player)
    {
        string json = JsonUtility.ToJson(player);

        reference.Child("Users").Child(id).Child("Achivement").SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Ghi dữ liệu người chơi thành công");
            }
            else
            {
                Debug.Log("Ghi dữ liệu người chơi thất bại: " + task.Exception);
            }
        });
    }

    public void ReadAcData(string id)
    {
        reference.Child("Users").Child(id).Child("Achivement").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    // Đọc dữ liệu từ Firebase
                    achivement = JsonUtility.FromJson<Achivement>(snapshot.GetRawJsonValue());

                   /* // Cập nhật dữ liệu vào UI ngay khi đọc thành công
                    AchivementManager ui = FindObjectOfType<AchivementManager>();
                    if (ui != null)
                    {
                        ui.achivement = achivement; // Cập nhật upgradeStats
                        ui.UpdateUI(); // Cập nhật UI ngay lập tức
                    }*/

                    Debug.Log("Đọc dữ liệu người chơi thành công.");

                }

            }

        });
    }
   

}
