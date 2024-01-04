using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float m_BasicRadius = 5f;
    private float m_CurrentRadius;

    private Turret[] turrets;
    private Destructible target;

    [SerializeField] private UpgradeAsset tempoUpgrade;
    [SerializeField] private float m_TempoRatio;
    [SerializeField] private UpgradeAsset rangeUpgrade;
    [SerializeField] private float m_RangeRatio;

    void Start()
    {
        turrets = GetComponentsInChildren<Turret>();

        var m_TempoLevel = Upgrades.GetUpgradeLevel(tempoUpgrade);        

        foreach (var turret in turrets)
        {
            turret.PushTheTempo(m_TempoLevel, m_TempoRatio);
        }

        var m_RangeLevel = Upgrades.GetUpgradeLevel(rangeUpgrade);
        
        m_CurrentRadius = m_BasicRadius * (1 + m_RangeRatio * m_RangeLevel);

        print("Радиус башни = " + m_CurrentRadius);
    }

    public void InitializeTowerDevelopment(TowerAsset[] towers)
    {        
        var buildSite = GetComponentInChildren<BuildSite>();
        buildSite.SetAvailableTowers(towers);
    }

    void Update()
    {
        if (target)
        {
            Vector2 targetVector = target.transform.position - transform.position;

            if (targetVector.magnitude <= m_CurrentRadius)
            {
                foreach (var turret in turrets)
                {
                    turret.transform.up = targetVector;
                    turret.Fire();
                }
            }
            else target = null;
        }
        else
        {
            var enter = Physics2D.OverlapCircle(transform.position, m_CurrentRadius);
            if (enter)
            {
                target = enter.transform.root.GetComponent<Destructible>();               
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, m_CurrentRadius);
    }
}
