using System;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : SingletonBase<Upgrades>
{
    public const string filename = "upgrades.dat";

    [Serializable]
   private class UpgradeSave
    {
        public UpgradeAsset m_Asset;
        public int level = 0;
    }

    [SerializeField] private UpgradeSave[] save;

    private new void Awake()
    {
        base.Awake();
        Saver<UpgradeSave[]>.TryLoad(filename, ref save);
        if (MapCompletion.Instance) MapCompletion.Instance.CalculateTotalScore();
    }
    public static void BuyUpgrade(UpgradeAsset asset)
    {        
        foreach (var upgrade in Instance.save)
        {
            if (upgrade.m_Asset == asset)
            {                
                upgrade.level += 1;

                Saver<UpgradeSave[]>.Save(filename, Instance.save);
            }
        }    
    }

    internal void ResetProgress()
    {
        foreach (var levelProgress in save)
        {
            levelProgress.level = 0;        
        }
    }

    public static int GetTotalCost()
    {
        int result = 0;
        foreach (var upgrade in Instance.save)
        {
            for (int i = 0; i < upgrade.level; i++)
            {
                result += upgrade.m_Asset.costByLevel[i];
            }            
        }
        //print("result =" + result);
        return result;        
    }
    public static int GetUpgradeLevel(UpgradeAsset asset)
    {
        foreach (var upgrade in Instance.save)
        {
            if (upgrade.m_Asset == asset)
            {
                return upgrade.level;
            }
        }
        return 0;
    }
}
