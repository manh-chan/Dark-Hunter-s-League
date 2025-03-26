using UnityEngine;
using UnityEngine.UI;

public class AudioTest : MonoBehaviour
{
    [Header("Tham chiếu đến các nút UI")]
    public Button StartGameButton;
    public Button EndGameButton;
    public Button LevelUpButton;
    public Button EnemyDeadButton;
    public Button ButtonClickButton;

    void Start()
    {
        // Gắn hàm xử lý cho mỗi nút
        StartGameButton.onClick.AddListener(() => AudioManager.Instance.StartGameMusic());
        EndGameButton.onClick.AddListener(() => AudioManager.Instance.PlayMenuMusic());
        LevelUpButton.onClick.AddListener(() => AudioManager.Instance.PlayLevelUp());
        EnemyDeadButton.onClick.AddListener(() => AudioManager.Instance.PlayEnemyDie());
        ButtonClickButton.onClick.AddListener(() => AudioManager.Instance.PlayButtonClick());
    }
}


/*void Update()
{

    if (Input.GetKeyDown(KeyCode.A))
    {
        AudioManager.Instance.PlayButtonClick();
    }


    if (Input.GetKeyDown(KeyCode.B))
    {
        AudioManager.Instance.PlayEnemyDie();
    }


    if (Input.GetKeyDown(KeyCode.C))
    {
        AudioManager.Instance.PlayLevelUp();
    }


    if (Input.GetKeyDown(KeyCode.Q))
    {
        AudioManager.Instance.StartGameMusic();
    }

    if (Input.GetKeyDown(KeyCode.E))
    {
        AudioManager.Instance.PlayMenuMusic();
    }
}*/