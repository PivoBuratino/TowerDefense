using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextUpdate : MonoBehaviour
{
    public enum UpdateSource { Gold, Life, Mana }
    public UpdateSource source = UpdateSource.Gold;
    private TMP_Text m_text;
    void Start()
    {
        m_text = GetComponent<TMP_Text>();
        switch (source)
        {
            case UpdateSource.Gold:
                TDPlayer.GoldUpdateSubscribe(UpdateText);
                break;
            case UpdateSource.Life:
                TDPlayer.LifeUpdateSubscribe(UpdateText);                
                break;
            case UpdateSource.Mana:
                TDPlayer.ManaUpdateSubscribe(UpdateText);
                break;
        }        
    }
    private void UpdateText(int money)
    {
        m_text.text = money.ToString();
    }    
}
