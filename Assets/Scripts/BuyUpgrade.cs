using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyUpgrade : MonoBehaviour
{
    [SerializeField] private UpgradeAsset asset;
    [SerializeField] public Image upgradeIcon;
    private int costNumber;
    [SerializeField] private TMP_Text level, costText;
    [SerializeField] private Button buyButton;

    public void Initialize()
    {
        print("zbs");
        upgradeIcon.sprite = asset.sprite;
        var savedLevel = Upgrades.GetUpgradeLevel(asset);        

        if (savedLevel >= asset.costByLevel.Length)
        {
            
            level.text = $"Lvl:" + savedLevel.ToString() + $"(Max)";
            buyButton.interactable = false;
            buyButton.transform.parent.Find("IconImage").gameObject.SetActive(false);
            buyButton.transform.Find("BuyText").gameObject.SetActive(false);
            costText.text = "X";
            costNumber = int.MaxValue;
        }
        else
        {            
            level.text = $"Lvl:" + (savedLevel + 1).ToString();
            costNumber = asset.costByLevel[savedLevel];
            costText.text = costNumber.ToString();
        }        
    }
    internal void CheckCost(int money)
    {
        buyButton.interactable = money >= costNumber;
    }
    public void Buy()
    {
        Upgrades.BuyUpgrade(asset);
        
        Initialize();
        
    }

}
