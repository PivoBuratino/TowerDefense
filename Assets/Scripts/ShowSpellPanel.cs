using UnityEngine;

public class ShowSpellPanel : MonoBehaviour
{
    [SerializeField] private GameObject spellPanel;
    private bool PanelOn;

    private void Start()
    {
        spellPanel.SetActive(false);
        PanelOn = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(1))
        {
            if (PanelOn == false)
            {             
                spellPanel.SetActive(true);
                PanelOn = true;
            }
            else
            {                
                spellPanel.SetActive(false);
                PanelOn = false;
            }
        }
    }
}
