using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Cinemachine.DocumentationSortingAttribute;

public class UpgradeUI : MonoBehaviour
{

    public UpgradeStats upgradeStats; 
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI upgradeCostText;
    public TextMeshProUGUI[] statTexts; 
    public Button[] upgradeButtons; 
    public int money = 1000; 
    public int totalSpentMoney;
    public int level;
    private int upgradeCost = 50; 

    private void Start()
    {
        UpdateUI();
        AssignButtonListeners();
        upgradeCostText.text = upgradeCost.ToString();
        levelText.text = level.ToString();
    }

    void AssignButtonListeners()
    {
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            int index = i;
            upgradeButtons[i].onClick.AddListener(() => UpgradeStat(index));
        }
    }
    public void UpdateMoney()
    {
        moneyText.text = money.ToString();
    }
    void UpgradeStat(int index)
    {
        if (money >= upgradeCost)
        {
            totalSpentMoney += upgradeCost;
            money -= upgradeCost;
            level++;
            upgradeCost = Mathf.FloorToInt(upgradeCost * 1.2f);
            switch (index)
            {
                case 0: upgradeStats.maxHealthBonus += 2f; break;
                case 1: upgradeStats.recoveryBonus += 0.02f; break;
                case 2: upgradeStats.armorBonus += 0.02f; ; break;
                case 3: upgradeStats.moveSpeedBonus += 0.02f; ; break;
                case 4: upgradeStats.mightBonus += 0.02f; ; break;
                case 5: upgradeStats.areaBonus += 0.02f; ; break;
                case 6: upgradeStats.speedBonus += 0.02f; ; break;
                case 7: upgradeStats.durationBonus += 0.02f; ; break;
                case 8: upgradeStats.cooldownBonus += 0.02f; ; break;
                case 9: upgradeStats.luckBonus += 0.02f; ; break;
            }

            UpdateUI();
            upgradeCostText.text = upgradeCost.ToString();
            levelText.text = level.ToString();
        }
    }
    public void ResetStats()
    {
        int refundMoney = Mathf.FloorToInt(totalSpentMoney * 0.8f); 
        money += refundMoney;
        totalSpentMoney = 0;

        
        upgradeStats.maxHealthBonus = 0;
        upgradeStats.recoveryBonus = 0;
        upgradeStats.armorBonus = 0;
        upgradeStats.moveSpeedBonus = 0;
        upgradeStats.mightBonus = 0;
        upgradeStats.areaBonus = 0;
        upgradeStats.speedBonus = 0;
        upgradeStats.durationBonus = 0;
        upgradeStats.cooldownBonus = 0;
        upgradeStats.luckBonus = 0;

        level = 0;
        upgradeCost = 50;
        upgradeCostText.text = upgradeCost.ToString();
        levelText.text = level.ToString();
        UpdateUI();
    }
    public void UpdateUI()
    {
        moneyText.text = money.ToString();

        foreach (Button btn in upgradeButtons)
        {
            btn.interactable = money >= upgradeCost;
        }

        statTexts[0].text = upgradeStats.maxHealthBonus.ToString();
        statTexts[1].text = upgradeStats.recoveryBonus.ToString("F2");
        statTexts[2].text = upgradeStats.armorBonus.ToString("F2");
        statTexts[3].text = upgradeStats.moveSpeedBonus.ToString("F2");
        statTexts[4].text = upgradeStats.mightBonus.ToString("F2");
        statTexts[5].text = upgradeStats.areaBonus.ToString("F2");
        statTexts[6].text = upgradeStats.speedBonus.ToString("F2");
        statTexts[7].text = upgradeStats.durationBonus.ToString("F2");
        statTexts[8].text = upgradeStats.cooldownBonus.ToString("F2");
        statTexts[9].text = upgradeStats.luckBonus.ToString("F2");
    }
}
