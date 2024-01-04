using UnityEngine;


[CreateAssetMenu]
public class TowerAsset : ScriptableObject
{
    public int goldCost = 15;
    public Sprite sprite;
    public Sprite GUISprite;
    public TurretProperties turretProperties;
    [SerializeField] private UpgradeAsset requiredUpgrade;
    [SerializeField] private int requiredUpgradeLevel;

    public TowerAsset[] m_UpgradesTo;
    public bool IsAvailable()
    {
        if (requiredUpgrade)
        {
            return requiredUpgradeLevel <= Upgrades.GetUpgradeLevel(requiredUpgrade);
        }
        else return true;
    }
}

