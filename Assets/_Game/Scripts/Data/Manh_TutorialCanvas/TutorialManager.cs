using UnityEngine;
using UnityEngine.UI; // Cần thiết để làm việc với UI Button

public class TutorialManager : MonoBehaviour
{
    // Kéo các Panel hướng dẫn từ Hierarchy vào đây trong Inspector
    public GameObject[] tutorialPanels;

    // Key để lưu trạng thái đã hoàn thành tutorial vào PlayerPrefs
    private const string TutorialCompletedKey = "TutorialCompleted";

    private int currentPanelIndex = 0;
    public bool tutorialCompleted = false;
    public GameObject panelToBringForward;
    private TutorialData UserData;

    void Start()
    {
        DontDestroyOnLoad(this);
        BringPanelToFront();
        string uid = PlayerPrefs.GetString("uid", "");
        UserFirebase.Instance.ReadTutorialFromFirebase(uid, (loadedData) =>
        {
            if (loadedData == null)
            {
                tutorialCompleted = false;
                ShowPanel(currentPanelIndex);
                UserData = new TutorialData(true);
                UserFirebase.Instance.WriteTutorialData(uid, UserData);
                Debug.Log("Bắt đầu Tutorial.");
            }
            else
            {
                HideAllPanels();
                tutorialCompleted = true;
                Debug.Log("Tutorial đã hoàn thành trước đó.");
            }
        });
        // Kiểm tra xem người chơi đã hoàn thành tutorial chưa
        // PlayerPrefs.GetInt trả về 0 nếu key chưa tồn tại (mặc định là chưa hoàn thành)
       
        if (tutorialCompleted)
        {
           
        }
        else
        {
            
        }
    }

    void Update()
    {
        BringPanelToFront();
    }
    void BringPanelToFront()
    {
        // Đưa panel này lên vị trí cuối cùng trong danh sách con của cha nó
        // -> Hiển thị trên cùng so với các anh em (siblings) khác.
        panelToBringForward.transform.SetAsLastSibling();
    }

    // Hàm hiển thị panel theo chỉ số (index)
    void ShowPanel(int index)
    {
        HideAllPanels(); // Ẩn hết các panel trước khi hiển thị panel mới

        if (index >= 0 && index < tutorialPanels.Length)
        {
            tutorialPanels[index].SetActive(true);
            currentPanelIndex = index; // Cập nhật index hiện tại
        }
        else
        {
            Debug.LogWarning("Index panel không hợp lệ: " + index);
        }
    }

    // Hàm này sẽ được gọi bởi Button "Tiếp tục"
    public void ShowNextPanel()
    {
        // Ẩn panel hiện tại
        if (currentPanelIndex >= 0 && currentPanelIndex < tutorialPanels.Length)
        {
            tutorialPanels[currentPanelIndex].SetActive(false);
        }

        // Tăng chỉ số để hiển thị panel tiếp theo
        currentPanelIndex++;

        // Kiểm tra xem còn panel nào không
        if (currentPanelIndex < tutorialPanels.Length)
        {
            ShowPanel(currentPanelIndex);
        }
        else
        {
            // Nếu hết panel, kết thúc tutorial
            EndTutorial();
        }
    }

    // Hàm này được gọi khi nhấn nút ở panel cuối cùng hoặc khi hoàn thành
    public void EndTutorial()
    {
        Debug.Log("Kết thúc Tutorial.");
        HideAllPanels(); // Ẩn tất cả các panel

        // Đánh dấu là đã hoàn thành tutorial
        PlayerPrefs.SetInt(TutorialCompletedKey, 1);
        PlayerPrefs.Save(); // Lưu thay đổi vào PlayerPrefs

        // Tùy chọn: Tiếp tục chạy game nếu đã dừng trước đó
        // Time.timeScale = 1f;

        // Tùy chọn: Có thể hủy GameObject này đi để tiết kiệm tài nguyên
        // Destroy(gameObject);
    }

    // Hàm ẩn tất cả các panel
    void HideAllPanels()
    {
        foreach (GameObject panel in tutorialPanels)
        {
            if (panel != null)
            {
                panel.SetActive(false);
            }
        }
    }
    public void HeroButton()
    {
        UIManager.Instance.OpenUI<CanvasHero>();

    }

    public void GrowthButton()
    {
        UIManager.Instance.OpenUI<CanvasGrowth>();

    }

    public void WeaponButton()
    {
        UIManager.Instance.OpenUI<CanvasWeapon>();

    }

    public void SelectHeroButton()
    {
        FindObjectOfType<CanvasHero>().gameObject.SetActive(false);

    }

    public void UpdateGrowButton()
    {
        FindObjectOfType<CanvasGrowth>().gameObject.SetActive(false);

    }

    public void SelectWeaponButton()
    {
        FindObjectOfType<CanvasWeapon>().gameObject.SetActive(false);

    }
}