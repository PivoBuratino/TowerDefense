using UnityEngine;

public class BuyControl : MonoBehaviour
{
    private RectTransform t;
    private void Awake()
    {
        t = GetComponent<RectTransform>();
        BuildSite.OnClickEvent += MoveToBuildSite;
        gameObject.SetActive(false);
    }
    private void MoveToBuildSite(Transform buildSite)
    {
        if (buildSite)
        {
            var position = Camera.main.WorldToScreenPoint(buildSite.position);
            print(position);
            t.anchoredPosition = position;
            gameObject.SetActive(true);
        }
        else gameObject.SetActive(false);

        foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
        {
            tbc.SetBuildSite(buildSite);
        }
    }
}
