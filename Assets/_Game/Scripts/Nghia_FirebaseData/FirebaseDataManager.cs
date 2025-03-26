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
            CharacterData player = ScriptableObject.CreateInstance<CharacterData>();

            // Gán giá trị từng thuộc tính
            player.Name = "Player123";
            player.CharacterDescription = "Player phu";
            player.FullName = "Player123 up";
            WritePlayerData("3", player);
        }

       
        if (Input.GetKeyDown(KeyCode.V))
        {
            ReadPlayerData("3");
        }
    }

    public void WritePlayerData(string id, CharacterData player)
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
                    Debug.Log("áđâsda");
                   
                    CharacterData player = ScriptableObject.CreateInstance<CharacterData>();
                    JsonUtility.FromJsonOverwrite(snapshot.GetRawJsonValue(), player);
                    ShowReadPlayerData("Đọc dữ liệu người chơi thành công: " + player.Name + ", characterDescription: " + player.CharacterDescription + ", fullName: " + player.FullName);
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
