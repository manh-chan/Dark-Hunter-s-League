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
                    CharacterData player = ScriptableObject.CreateInstance<CharacterData>();
                    JsonUtility.FromJsonOverwrite(snapshot.GetRawJsonValue(), player);
                    ShowReadPlayerData($"Đọc thành công: {player.Name}, {player.CharacterDescription}, {player.FullName}");
                }
                else
                {
                    ShowReadPlayerData("Dữ liệu ngườii chơi không tồnn tại!");
                }
            }
            else
            {
                ShowReadPlayerData("Đọc dữ liệu thất bại: " + task.Exception);
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
