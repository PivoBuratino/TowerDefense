using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeShop : MonoBehaviour
{
    [SerializeField] private int money;  
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private BuyUpgrade[] sales;

    private void Start()
    {        
        foreach (var slot in sales)
        {
            slot.Initialize();
            slot.transform.Find("Button").GetComponent<Button>().onClick.AddListener(UpdateMoney);
        }
        UpdateMoney();
    }
    private void UpdateMoney()
    {
        money = MapCompletion.Instance.TotalScore;
        money -= Upgrades.GetTotalCost();
        moneyText.text = money.ToString();
        foreach (var slot in sales)
        {
            slot.CheckCost(money);
        }
    }
}
