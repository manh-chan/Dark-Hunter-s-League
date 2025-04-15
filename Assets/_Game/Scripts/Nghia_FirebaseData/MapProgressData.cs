using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapProgressData
{
    public List<bool> unlockedMaps;

    public MapProgressData(List<bool> unlocked)
    {
        this.unlockedMaps = unlocked;
    }
    public MapProgressData(int totalMaps)
    {
        unlockedMaps = new List<bool>();
        for (int i = 0; i < totalMaps; i++)
        {
            unlockedMaps.Add(false);
        }
    }
}
