using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class UICharactedSelector : Singleton<UICharactedSelector>
{
    public CharacterData defaultCharacter;
    public static CharacterData selected;
    public UiStatsDisplay statsUI;

    private CharProgressData charProgressData;

    [Header("Template")]
    public Toggle toggleTemplate;
    public string characterNamePath = "Character Name";
    public string weaponIconPath = "Weapon Icon";
    public string characterIconPath = "Character Icon";
    public string price = "Character Price";
    public List<Toggle> selectableToggles = new List<Toggle>();

    [Header("DescriptionBox")]
    public TMP_Text characterFullName;
    public TMP_Text characterDescription;
    public Image selectedCharacterIcon;
    public Image selectedCharacterWeapon;

    private void Start()
    {
        string uid = PlayerPrefs.GetString("uid", "");

        FirebaseDataManager.Instance.LoadCharProgressFromFirebase(uid, (loadedData) =>
        {
            if (loadedData == null || loadedData.unlockedChars.Count != selectableToggles.Count)
            {
                // Nếu không có hoặc sai kích thước, tạo mới
                charProgressData = new CharProgressData(selectableToggles.Count);
                FirebaseDataManager.Instance.SaveCharProgressToFirebase(uid, charProgressData);
            }
            else
            {
                charProgressData = loadedData;
            }

            UpdateUI();
        });
    }
    private void UpdateUI()
    {
        for (int i = 0; i < selectableToggles.Count; i++)
        {
            if (i < charProgressData.unlockedChars.Count && charProgressData.unlockedChars[i])
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
    public static CharacterData[] GetAllCharacterDataAssets()
    {
        List<CharacterData> characters = new List<CharacterData>();
#if UNITY_EDITOR
        string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
        foreach (string assetPath in allAssetPaths)
        {
            if (assetPath.EndsWith(".asset"))
            {
                CharacterData characterData = AssetDatabase.LoadAssetAtPath<CharacterData>(assetPath);
                if (characterData != null) characters.Add(characterData);
            }
        }
#else
        Debug.LogWarning("This function cannot bo called on builds");
#endif
        return characters.ToArray();
    }
    public static CharacterData GetData()
    {
        if (selected)
        {
            return selected;
        }
        else
        {
            CharacterData[] characters = GetAllCharacterDataAssets();
            if (characters.Length > 0) return characters[Random.Range(0, characters.Length)];
        }
        return null;
    }
    public void Select(CharacterData character)
    {
        selected = statsUI.character = character;
        statsUI.UpdateFields();

        characterFullName.text = character.FullName;
        characterDescription.text = character.CharacterDescription;
        selectedCharacterIcon.sprite = character.Icon;
        selectedCharacterWeapon.sprite = character.StartingWeapon.icon;
    }
    public void UnlockNextChar(int currentIndex)
    {
        string uid = PlayerPrefs.GetString("uid", "");

        // Tải dữ liệu hiện tại trước khi sửa
        FirebaseDataManager.Instance.LoadCharProgressFromFirebase(uid, (loadedData) =>
        {
            if (loadedData == null || loadedData.unlockedChars.Count == 0)
            {
                charProgressData = new CharProgressData(5); // Tạo mới nếu chưa có gì
            }
            else
            {
                charProgressData = loadedData; // Dùng dữ liệu đã lưu
            }

            // Kiểm tra và mở khóa map tiếp theo
            if (currentIndex + 1 < charProgressData.unlockedChars.Count)
            {
                charProgressData.unlockedChars[currentIndex + 1] = true;

                FirebaseDataManager.Instance.SaveCharProgressToFirebase(uid, charProgressData);
                UpdateUI();
            }
        });
    }
}
