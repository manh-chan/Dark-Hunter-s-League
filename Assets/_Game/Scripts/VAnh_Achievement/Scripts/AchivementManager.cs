using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchivementManager : MonoBehaviour
{
    public GameObject achievementPrefab;
    public Sprite[] sprites;
    private AchieveButton activeButton;
    public ScrollRect scrollRect;
    public GameObject visualAchievement;
    public Dictionary<string, Achivement> achievements = new Dictionary<string, Achivement>();
    public Sprite unlockedSprite;

    public TextMeshProUGUI textPoints;

    private static AchivementManager instance;
    private float fadeTime = 1.5f;
    public static AchivementManager Instance {

        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<AchivementManager>();
            }
            return AchivementManager.instance;
        }     
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        activeButton = GameObject.Find("GeneralBtn").GetComponent<AchieveButton>();
        CreateAchievement("General","Chào mừng người chơi mới","Chơi ván game đầu tiên.",5,0);
        CreateAchievement("General", "Tân Binh Ra Trận", "Hoàn thành Map 1.", 10, 1);
        CreateAchievement("General", "Lính Mới Đã Thành Thạo", "Hoàn thành Map 2.", 10, 5);
        CreateAchievement("General", "Kẻ Sống Sót", "Hoàn thành Map 3.", 15, 6);
        //hoan thanh 3 thanh tuu tren de mo khoa
        CreateAchievement("General", "Kẻ Chiến Thắng", "Hoàn thành Toàn Bộ Màn Chơi.", 100, 7, new string[] { "Tân Binh Ra Trận", "Lính Mới Đã Thành Thạo", "Kẻ Sống Sót" });


        CreateAchievement("Other", "Kẻ Diệt Quái", "Tiêu diệt 100 quái vật.", 5, 3);
        CreateAchievement("Other", "Bố Của Lũ Quái", "Tiêu diệt 500 quái vật.", 10, 3);
        CreateAchievement("Other", "Thần Săn Quái", "Tiêu diệt 1000 quái vật.", 20, 3);
        CreateAchievement("Other", "Kẻ hủy diệt", "Tiêu diệt 5000 quái vật.", 50, 3);

        foreach (GameObject achievementList in GameObject.FindGameObjectsWithTag("AchievementList"))
        {
            achievementList.SetActive(false);
        }
        activeButton.Select();

    }

    // Update is called once per frame
    void Update()
    {
        //thuc hien nhiem vu de nhan dc thanh tuu
        if(Input.GetKeyDown(KeyCode.Q))
        {
            EarnAchievement("Chào mừng người chơi mới");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            EarnAchievement("Tân Binh Ra Trận");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            EarnAchievement("Lính Mới Đã Thành Thạo");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            EarnAchievement("Kẻ Sống Sót");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            EarnAchievement("Thần Săn Quái");
        }
    }
    //dat duoc thanh tuu
    public void EarnAchievement(string title)
    {
        if (achievements[title].EarnAchievement())
        {
            GameObject achievement = Instantiate(visualAchievement);
            SetAchievementInfor("EarnCanvas", achievement, title);
            textPoints.text = "Points: " + PlayerPrefs.GetInt("Points");
            StartCoroutine(FadeAchievement(achievement));
        }
    }
    //an achievement
    public IEnumerator HideAchiement(GameObject achievement)
    {
        yield return new WaitForSeconds(3);
        Destroy(achievement);
    }
    public void CreateAchievement(string parent, string title,string description, int points, int spriteIndex, string[] dependencies = null)
    {
        GameObject achievement = (GameObject)Instantiate(achievementPrefab);

        Achivement  newAchivement = new Achivement(title, description, points, spriteIndex, achievement);
        achievements.Add(title, newAchivement);
        SetAchievementInfor(parent, achievement, title);
        if (dependencies != null)
        {
            foreach (string achievementTitle in dependencies)
            {
                Achivement dependency = achievements[achievementTitle];
                dependency.Child = title;
                newAchivement.AddDependency(dependency);
            }
        }
    }
    //set achievement infor vao prefab
    public void SetAchievementInfor(string parent, GameObject achievement, string title)
    {
        achievement.transform.SetParent(GameObject.Find(parent).transform);
        achievement.transform.localScale = new Vector3(1,1,1);
        achievement.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = title;
        achievement.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = achievements[title].Description;
        achievement.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = achievements[title].Points.ToString();
        achievement.transform.GetChild(4).GetComponent<Image>().sprite = sprites[achievements[title].SpriteIndex];

    }
    //button doi giua general voi other
    public void ChangeCategory(GameObject button)
    {
        AchieveButton achieveButton = button.GetComponent<AchieveButton>();
        scrollRect.content = achieveButton.achievementList.GetComponent<RectTransform>();
        activeButton.Deselect();
        achieveButton.Select();
        activeButton = achieveButton;
    }
    //hieu ung mo dan` di
    private IEnumerator FadeAchievement(GameObject achievement)
    {
        CanvasGroup canvasGroup = achievement.GetComponent<CanvasGroup>();
        float rate = 1.0f / fadeTime;

        int startAlpha = 0;
        int endAlpha = 1;


        for(int i = 0; i < 2; i++)
        {
            float progress = 0.0f;

            while (progress < 1.0)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, progress);

                progress += rate * Time.deltaTime;

                yield return null;
            }
            yield return new WaitForSeconds(2);
            startAlpha = 1;
            endAlpha = 0;
        }
        Destroy(achievement);
    }
}
