using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPrefab : MonoBehaviour
{
    public GameObject[] maps; 
    private GameObject currentMap; 
    private int currentMapIndex = 0; 

    private void Start()
    {
        LoadMap(currentMapIndex);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            LoadNextMap();
        }
    }

    void LoadNextMap()
    {
        if (currentMap != null)
        {
            Destroy(currentMap);
        }

        currentMapIndex = (currentMapIndex + 1) % maps.Length; 
        LoadMap(currentMapIndex);
    }

    void LoadMap(int index)
    {
        currentMap = Instantiate(maps[index], Vector3.zero, Quaternion.identity); 
        currentMap.tag = "SceneContainer";
    }
}
