using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserFirebase : Singleton<UserFirebase>
{
    public SkipDataUser player;

    private DatabaseReference reference;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void WritePlayerData(string id, SkipDataUser player)
    {
        string json = JsonUtility.ToJson(player);

        reference.Child("Users").Child(id).Child("CheckUser").SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
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

    public void ReadUserProgressFromFirebase(string uid, Action<SkipDataUser> onLoaded)
    {
        reference.Child("Users").Child(uid).Child("CheckUser").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && task.Result.Exists)
            {
                string json = task.Result.GetRawJsonValue();
                SkipDataUser data = JsonUtility.FromJson<SkipDataUser>(json);
                onLoaded?.Invoke(data);
            }
            else
            {
                Debug.Log("⚠️ Không có dữ liệu, tạo mới.");
                onLoaded?.Invoke(null);
            }
        });
    }
}