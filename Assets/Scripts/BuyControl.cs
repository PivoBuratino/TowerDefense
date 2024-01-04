using UnityEngine;
using System.Collections.Generic;

public class BuyControl : MonoBehaviour
{
    [SerializeField] private TowerBuyControl m_TowerBuyPrefab;    

    private List<TowerBuyControl> m_ActiveControl;
    public RectTransform m_RectTransform;
    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();

        BuildSite.OnClickEvent += MoveToBuildSite;
        gameObject.SetActive(false);

    }
    private void MoveToBuildSite(BuildSite buildSite)
    {  
        if (buildSite)
        {
            if (m_ActiveControl != null)
            {
                foreach (var control in m_ActiveControl) Destroy(control.gameObject);
            }
            
            var position = Camera.main.WorldToScreenPoint(buildSite.transform.root.position);

            m_RectTransform.anchoredPosition = position;
            gameObject.SetActive(true);
            m_ActiveControl = new List<TowerBuyControl>();
            foreach (var asset in buildSite.availableTowers)
            {
                if (asset.IsAvailable())
                {
                    print("Башня" + asset.name + "доступна");
                    var newControl = Instantiate(m_TowerBuyPrefab, transform);
                    m_ActiveControl.Add(newControl);
                    newControl.SetTowerAsset(asset);
                }
            }
            var angle = 360 / m_ActiveControl.Count;
            for (int i = 0; i < m_ActiveControl.Count; i++)
            {
                var offset = Quaternion.AngleAxis(angle * i, Vector3.forward) * (Vector3.up * 80);
                
                m_ActiveControl[i].transform.position += offset;
                print("позиция кнопки = " + m_ActiveControl[i].transform.position);
            }
            
            float offsetX = 0;
            float offsetY = 0;
            for (int i = 0; i < m_ActiveControl.Count; i++)
            {                
                if (m_ActiveControl[i].transform.position.x < 120)
                {
                    var currentOffsetX = 125 - m_ActiveControl[i].transform.position.x;
                    offsetX = Mathf.Max(offsetX, currentOffsetX);
                }
                if (m_ActiveControl[i].transform.position.y < 120)
                {
                    var currentOffsetY = 125 - m_ActiveControl[i].transform.position.y;
                    offsetY = Mathf.Max(offsetY, currentOffsetY);
                }
            }
            for (int i = 0; i < m_ActiveControl.Count; i++)
            {
                m_ActiveControl[i].transform.Translate(offsetX, offsetY, 0);
            }
            
        }
        else if (gameObject == isActiveAndEnabled && m_ActiveControl != null)
        {
            foreach (var control in m_ActiveControl) Destroy(control.gameObject);
            m_ActiveControl.Clear();
            gameObject.SetActive(false);
        }
        foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
        {
            tbc.SetBuildSite(buildSite.transform.root);
        }
    }
    public void OnDestroy()
    {
        BuildSite.OnClickEvent -= MoveToBuildSite;
    }
}
