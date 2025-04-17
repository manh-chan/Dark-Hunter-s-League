using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CharProgressData
{
    public List<bool> unlockedChars = new List<bool>();

    public CharProgressData(int totalChars)
    {
        for (int i = 0; i < totalChars; i++)
        {
            unlockedChars.Add(false);
        }
        unlockedChars[0] = true; // Mặc định mở map đầu
    }
}
