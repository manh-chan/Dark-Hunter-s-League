using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using UnityEngine;

public class FirebaseDataManager : MonoBehaviour
{
    private DatabaseReference reference;

    private void Awake()
    {
        // Khởi tạo Firebase
        FirebaseApp app = FirebaseApp.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Start()
    {
        PlayerData player = new PlayerData("Warrior123", 10, 150, 3200);
        WritePlayerData("1", player);

        ReadPlayerData("1");
    }

    public void WritePlayerData(string id, PlayerData player)
    {
        string json = JsonUtility.ToJson(player); 

        reference.Child("Users").Child(id).Child("Player").SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Ghi dữ liệu người chơi thành công");
            }
            else
            {
                Debug.LogError("Ghi dữ liệu người chơi thất bại: " + task.Exception);
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
                    Debug.Log("Đọc dữ liệu người chơi thành công: " + player.name + ", Level: " + player.level + ", HP: " + player.hp + ", XP: " + player.xp);
                }
                else
                {
                    Debug.Log("Dữ liệu người chơi không tồn tại!");
                }
            }
            else
            {
                Debug.LogError("Đọc dữ liệu người chơi thất bại: " + task.Exception);
            }
        });
    }
}
