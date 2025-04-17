using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class AchieveButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject achievementList;
    public Sprite neutral, highlight;
    private Image buttonImage;

    void Awake()
    {
        buttonImage = GetComponent<Image>();
    }
    public void Select()
    {
        buttonImage.sprite = highlight;
        achievementList.SetActive(true);
    }

    public void Deselect()
    {
        buttonImage.sprite = neutral;
        achievementList.SetActive(false);
    }

    //public void Click()
    //{
    //    if(buttonImage.sprite == neutral)
    //    {
    //        buttonImage.sprite = highlight;
    //        achievementList.SetActive(true);
    //    }
    //    else
    //    {
    //        buttonImage.sprite= neutral;
    //        achievementList.SetActive(false);
    //    }
    //}
}
