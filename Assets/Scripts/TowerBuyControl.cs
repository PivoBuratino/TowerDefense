using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerBuyControl : MonoBehaviour
{
    [SerializeField] private TowerAsset m_ta;
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
    
        m_Text.text = m_ta.goldCost.ToString();
        m_Button.GetComponent<Image>().sprite = m_ta.GUISprite;
    }
    private void GoldStatusCheck (int gold)
    {
        if (gold > m_ta.goldCost != m_Button.interactable)
        {
            m_Button.interactable = !m_Button.interactable;
            m_Text.color = m_Button.interactable ? Color.white : Color.red;
        }
    }
    public void Buy()
    {
        TDPlayer.Instance.TryBuild(m_ta, buildSite);
        BuildSite.HideControls();
    }
}
