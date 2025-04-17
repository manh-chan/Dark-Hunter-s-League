using System.Collections.Generic;

[System.Serializable]
public class MapProgressData
{
    public List<bool> unlockedMaps = new List<bool>();

    public MapProgressData(int totalMaps)
    {
        for (int i = 0; i < totalMaps; i++)
        {
            unlockedMaps.Add(false);
        }
        unlockedMaps[0] = true; // Mặc định mở map đầu
    }
}