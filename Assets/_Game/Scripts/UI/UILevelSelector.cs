﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILevelSelector : MonoBehaviour
{
    public UISceneDataDisplay statsUI;

    public static int selectedLevel = -1;
    public static SceneData currentLevel;
    public List<SceneData> levels = new List<SceneData>();
    protected int currentMap;
    MapProgressData mapProgressData;

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
        public SceneAsset scene;

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
    public static SceneAsset[] GetAllMaps()
    {
        List<SceneAsset> maps = new List<SceneAsset>();

#if UNITY_EDITOR
        string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
        foreach (string assetPath in allAssetPaths)
        {
            if (assetPath.EndsWith(".unity"))
            {
                SceneAsset map = AssetDatabase.LoadAssetAtPath<SceneAsset>(assetPath);
                if(map != null && Regex.IsMatch(map.name,MAP_NAME_FORMAT)) maps.Add(map);
            }
        }
#else
Debug.LogWarning("this function cannot be called on builds.");
#endif
        maps.Reverse();
        return maps.ToArray();
    }

    public virtual void Awake()
    {
        string uid = PlayerPrefs.GetString("uid", "");
        mapProgressData = new MapProgressData(levels.Count);
        mapProgressData.unlockedMaps[0] = true;
        FirebaseDataManager.Instance.SaveMapProgressToFirebase(uid, mapProgressData.unlockedMaps);

        FirebaseDataManager.Instance.LoadMapProgressFromFirebase(uid, (unlockedMaps) =>
        {
            for (int i = 0; i < selectableToggles.Count; i++)
            {
                if (i < unlockedMaps.Count && unlockedMaps[i])
                {
                    selectableToggles[i].interactable = true;
                    selectableToggles[i].image.color = Color.green;
                }
                else
                {
                    selectableToggles[i].interactable = false;
                    selectableToggles[i].image.color = Color.red;
                }
            }
        });
    }
    public void SceneChange(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1f;
    }
    public void LoadselectedLevel()
    {
        if(selectedLevel >= 0)
        {
            SceneManager.LoadScene(levels[selectedLevel].scene.name);
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
        foreach ( FieldInfo f in fields )
        {
            object val = f.GetValue(obj);
            if (val is int) sum += (int)val;
            else if (val is float) sum += (float)val;
        }
        return Mathf.Approximately(sum, 0);
    }

   
}
