using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerBuyControl : MonoBehaviour
{
    [SerializeField] private TowerAsset m_TowerAsset;
    public void SetTowerAsset(TowerAsset asset) { m_TowerAsset = asset; }
    [SerializeField] private TMP_Text m_Text;
    [SerializeField] private Button m_Button;
    [SerializeField] private Transform buildSite;
    public void SetBuildSite(Transform value)
    { 
        buildSite = value; 
    }
        
    private void Start()
    {
        TDPlayer.GoldUpdateSubscribe(GoldStatusCheck);
    
        m_Text.text = m_TowerAsset.goldCost.ToString();
        m_Button.GetComponentInChildren<Image>().sprite = m_TowerAsset.GUISprite;
    }
    private void OnDestroy()
    {
        TDPlayer.GoldUpdateUnSubscribe(GoldStatusCheck);
    }
    private void GoldStatusCheck (int gold)
    {
        if (gold >= m_TowerAsset.goldCost)
        {
            m_Button.interactable = true;
            m_Text.color = Color.white;
        }
        else
        {
            m_Button.interactable = false;
            m_Text.color = Color.red;
        }
    }
    public void Buy()
    {
        TDPlayer.Instance.TryBuild(m_TowerAsset, buildSite);        
        BuildSite.HideControls();
    }  
}
