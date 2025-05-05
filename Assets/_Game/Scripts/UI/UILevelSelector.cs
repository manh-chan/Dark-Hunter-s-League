using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class UILevelSelector : Singleton<UILevelSelector>
{
    public UISceneDataDisplay statsUI;
    public static int selectedLevel = -1;
    public static SceneData currentLevel;
    public List<SceneData> levels = new List<SceneData>();
    protected int currentMap;
    public MapProgressData mapProgressData;

    [Header("Template")]
    public Toggle toggleTemplate;
    public string LevelNamePath = "Level Name";
    public string LevelNumberPath = "Level Number";
    public string LevelDescriptionPath = "Level Description";
    public string LevelImagePath = "Level Image";
    public List<Toggle> selectableToggles = new List<Toggle>();

    public static BuffData globaBuff;

    public static bool globalBuffAffectsPlayer = false, globalBuffAffectsEnemies = false;

    public const string MAP_NAME_FORMAT = "^(Level .*?) ?- ?(.*)$";

    [System.Serializable]
    public class SceneData
    {
#if UNITY_EDITOR
        public SceneAsset scene; // Chỉ sử dụng trong Editor
#endif

        public string sceneName; // Dùng cho việc load scene khi build

        [Header("UI Display")]
        public string displayName;
        public string label;
        [TextArea] public string description;
        public Sprite icon;

        [Header("Modifiers")]
        public CharacterData.Stats playerModifiers;
        public EnemyState.Stats enemyModifiers;
        [Min(-1)] public float timeLimit = 0f, clockSpeed = 1f;
        [TextArea] public string extraNotes = "--";
    }

#if UNITY_EDITOR
    public static SceneAsset[] GetAllMaps()
    {
        List<SceneAsset> maps = new List<SceneAsset>();

        string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
        foreach (string assetPath in allAssetPaths)
        {
            if (assetPath.EndsWith(".unity"))
            {
                SceneAsset map = AssetDatabase.LoadAssetAtPath<SceneAsset>(assetPath);
                if (map != null && Regex.IsMatch(map.name, MAP_NAME_FORMAT)) maps.Add(map);
            }
        }

        maps.Reverse();
        return maps.ToArray();
    }
#else
    public static object[] GetAllMaps()
    {
        Debug.LogWarning("This function cannot be called on builds.");
        return null;
    }
#endif

    public virtual void Awake()
    {
        string uid = PlayerPrefs.GetString("uid", "");

        FirebaseDataManager.Instance.LoadMapProgressFromFirebase(uid, (loadedData) =>
        {
            if (loadedData == null || loadedData.unlockedMaps.Count != levels.Count)
            {
                mapProgressData = new MapProgressData(levels.Count);
                FirebaseDataManager.Instance.SaveMapProgressToFirebase(uid, mapProgressData);
            }
            else
            {
                mapProgressData = loadedData;
            }

            UpdateUI();
        });

#if UNITY_EDITOR
        // Đồng bộ sceneName từ scene (chỉ trong editor)
        foreach (var level in levels)
        {
            if (level.scene != null)
            {
                level.sceneName = level.scene.name;
            }
        }
#endif
    }

    private void UpdateUI()
    {
        for (int i = 0; i < selectableToggles.Count; i++)
        {
            if (i < mapProgressData.unlockedMaps.Count && mapProgressData.unlockedMaps[i])
            {
                selectableToggles[i].interactable = true;
                selectableToggles[i].image.color = new Color32(140, 140, 140, 255);
            }
            else
            {
                selectableToggles[i].interactable = false;
                selectableToggles[i].image.color = new Color32(67, 67, 67, 255);
            }
        }
    }

    public void SceneChange(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1f;
    }

    public void LoadselectedLevel()
    {
        if (selectedLevel >= 0)
        {
            SceneManager.LoadScene(levels[selectedLevel].sceneName);
            currentLevel = levels[selectedLevel];
            selectedLevel = -1;
            Time.timeScale = 1f;
        }
        else
        {
            Debug.Log("No level was selected!");
        }
    }

    public void Select(int sceneIndex)
    {
        selectedLevel = sceneIndex;
        statsUI.UpdateFields();
        globaBuff = GenerateGlobalBuffData();
        globalBuffAffectsPlayer = globaBuff && IsModifierEmty(globaBuff.variations[0].playerModifier);
        globalBuffAffectsEnemies = globaBuff && IsModifierEmty(globaBuff.variations[0].enemyModifier);
    }

    public BuffData GenerateGlobalBuffData()
    {
        BuffData bd = ScriptableObject.CreateInstance<BuffData>();
        bd.name = "Global Level Buff";
        bd.variations[0].damagePerSecond = 0;
        bd.variations[0].duration = 0;
        bd.variations[0].playerModifier = levels[selectedLevel].playerModifiers;
        bd.variations[0].enemyModifier = levels[selectedLevel].enemyModifiers;
        return bd;
    }

    private static bool IsModifierEmty(object obj)
    {
        Type type = obj.GetType();
        FieldInfo[] fields = type.GetFields();
        float sum = 0;
        foreach (FieldInfo f in fields)
        {
            object val = f.GetValue(obj);
            if (val is int) sum += (int)val;
            else if (val is float) sum += (float)val;
        }
        return Mathf.Approximately(sum, 0);
    }

    public void UnlockNextMap(int currentMapIndex)
    {
        string uid = PlayerPrefs.GetString("uid", "");

        // Tải dữ liệu hiện tại trước khi sửa
        FirebaseDataManager.Instance.LoadMapProgressFromFirebase(uid, (loadedData) =>
        {
            if (loadedData == null || loadedData.unlockedMaps.Count == 0)
            {
                mapProgressData = new MapProgressData(15); // Tạo mới nếu chưa có gì
            }
            else
            {
                mapProgressData = loadedData; // Dùng dữ liệu đã lưu
            }

            // Kiểm tra và mở khóa map tiếp theo
            if (currentMapIndex + 1 < mapProgressData.unlockedMaps.Count)
            {
                mapProgressData.unlockedMaps[currentMapIndex + 1] = true;

                FirebaseDataManager.Instance.SaveMapProgressToFirebase(uid, mapProgressData);
                UpdateUI();
            }
        });
    }
}
