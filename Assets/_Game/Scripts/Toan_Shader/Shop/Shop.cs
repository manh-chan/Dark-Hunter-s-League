using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [System.Serializable] class ShopItem
    {
        public Sprite Image;
        public int Price;
        public bool IsPurchased = false;
    }

    [SerializeField] List<ShopItem> ShopItemsList;
    [SerializeField] Text CoinsText;




    GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;
    Button buyBtn;

    private void Start()
    {
       ItemTemplate = ShopScrollView.GetChild(0).gameObject;
        int len = ShopItemsList.Count;
        for (int i = 0; i < len; i++)
        {
            g = Instantiate(ItemTemplate, ShopScrollView);
            g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].Image;
            g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = ShopItemsList[i].Price.ToString();
            buyBtn = g.transform.GetChild(2).GetComponent<Button>();
            buyBtn.interactable = !ShopItemsList[i].IsPurchased;
            buyBtn.AddEventListener(i, OnShopItemBtnClicked);
        }
        Destroy(ItemTemplate);
        SetCoinsUI();
    }

    void OnShopItemBtnClicked(int itemIndex)
    {
        if (Game.Instance.HasEnoughCoins(ShopItemsList[itemIndex].Price))
        {
            Game.Instance.UseCoins(ShopItemsList[itemIndex].Price);
            ShopItemsList[itemIndex].IsPurchased = true;


            ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>().interactable = false;

            /* buyBtn = ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
             buyBtn.interactable = false;
             buyBtn.transform.GetChild(0).GetComponent<Text>().text = "Purchased";*/

            Image iconImage = ShopScrollView.GetChild(itemIndex).GetChild(0).GetComponent<Image>();// lam sang
            Color color = iconImage.color;
            color.a = 1f; 
            iconImage.color = color;


            SetCoinsUI();
        }
        else
        {
            Debug.Log("tgrnihjuurgtdhu");
        }  
    }
   void SetCoinsUI()
    {
        CoinsText.text = Game.Instance.Coins.ToString();
    }
}
