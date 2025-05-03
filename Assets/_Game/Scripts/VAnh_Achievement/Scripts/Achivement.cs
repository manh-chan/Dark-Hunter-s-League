
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achivement : MonoBehaviour
{
    private string name;
    private string description;
    private bool unlocked;
    private int points;
    private int spriteIndex;
    private GameObject achievementRef;
    private List<Achivement> dependencies = new List<Achivement>();
    private string child;
    public string Child
    {
        get
        {
            return child;
        }
        set
        {
            child = value;
        }
    }

    public Achivement(string name, string description, int points, int spriteIndex, GameObject achievementRef)
    {
        this.Name = name;
        this.Description = description;
        this.Unlocked = false;
        this.Points = points;
        this.SpriteIndex = spriteIndex;
        this.AchievementRef = achievementRef;
        LoadAchievement();
    }
    public void AddDependency(Achivement dependency)
    {
        dependencies.Add(dependency);
    }

    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public bool Unlocked { get => unlocked; set => unlocked = value; }
    public int Points { get => points; set => points = value; }
    public int SpriteIndex { get => spriteIndex; set => spriteIndex = value; }
    public GameObject AchievementRef { get => achievementRef; set => achievementRef = value; }

    public bool EarnAchievement()
    {
        if (!unlocked && !dependencies.Exists(x => x.unlocked == false))
        {
            achievementRef.GetComponent<Image>().sprite = AchivementManager.Instance.unlockedSprite;
            SaveAchievement(true);
            if (child != null)
            {
                AchivementManager.Instance.EarnAchievement(child);
            }
            return true;
        }
        return false;
    }
    //
    //hien dang dung playerprefs save bang firebase o day
    public void SaveAchievement(bool value)
    {
        unlocked = value;
        int tmpPoints = PlayerPrefs.GetInt("Points");
        PlayerPrefs.SetInt("Points", tmpPoints += points);
        PlayerPrefs.SetInt(name, value ? 1 : 0);
        PlayerPrefs.Save();
    }
    public void LoadAchievement()
    {
        unlocked = PlayerPrefs.GetInt(name) == 1 ? true : false;
        if (unlocked)
        {
            //unlocked = true thi load du lieu
            AchivementManager.Instance.textPoints.text = "Points: " + PlayerPrefs.GetInt("Points");
            achievementRef.GetComponent<Image>().sprite = AchivementManager.Instance.unlockedSprite;

        }
    }
    // Start is called before the first frame update

}
