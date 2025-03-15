using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseData : MonoBehaviour
{
    private DatabaseReference reference;

    private void Awake()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseApp app = FirebaseApp.DefaultInstance;
    }

    void Start()
    {
        WriteDatabase("123", "hello");
    }

    public void WriteDatabase(string id, string messsage)
    {
        reference.Child("User").Child(id).SetValueAsync(messsage).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("ghi data thanh cong");
            }
            else
            {
                Debug.Log("ghi data that bai" + task.Exception);
            }
        }) ;
    }

    public void ReadDatabase(string id)
    {

    }
}
