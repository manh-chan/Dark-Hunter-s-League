using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseDataManager : MonoBehaviour
{
    private DatabaseReference reference;

    public TextMeshProUGUI showReadPlayerData;
    public TextMeshProUGUI showWritePlayerData;

    private void Awake()
    {
        // Khởi tạo Firebase
        FirebaseApp app = FirebaseApp.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.G))
        {
            PlayerData player = new PlayerData("Warrior123", 10, 150, 3200);
            WritePlayerData("2", player);
        }

       
        if (Input.GetKeyDown(KeyCode.D))
        {
            ReadPlayerData("1");
        }
    }

    public void WritePlayerData(string id, PlayerData player)
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
                   
                    PlayerData player = JsonUtility.FromJson<PlayerData>(snapshot.GetRawJsonValue());
                    ShowReadPlayerData("Đọc dữ liệu người chơi thành công: " + player.name + ", Level: " + player.level + ", HP: " + player.hp + ", XP: " + player.xp);
                }
                else
                {
                    ShowReadPlayerData("Dữ liệu người chơi không tồn tại!");
                }
            }
            else
            {
                ShowReadPlayerData("Đọc dữ liệu người chơi thất bại: " + task.Exception);
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
