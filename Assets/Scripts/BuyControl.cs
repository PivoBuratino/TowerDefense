using UnityEngine;

public class BuyControl : MonoBehaviour
{
    public RectTransform t;
    private void Awake()
    {
        t = GetComponent<RectTransform>();

        BuildSite.OnClickEvent += MoveToBuildSite;
        gameObject.SetActive(false);
    }    
    private void MoveToBuildSite(Transform buildSite)
    {        
        if (buildSite != null)
        {
            var position = Camera.main.WorldToScreenPoint(buildSite.position);
            
            t.anchoredPosition = position;
            gameObject.SetActive(true);
        }
        else gameObject.SetActive(false);

        foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
        {
            tbc.SetBuildSite(buildSite);
        }
    }
    public void OnDestroy()
    {
        BuildSite.OnClickEvent -= MoveToBuildSite;
    }
}
