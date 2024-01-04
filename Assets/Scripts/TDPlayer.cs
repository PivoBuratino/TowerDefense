using System;
using UnityEngine;
using System.Collections.Generic;

public class TDPlayer : Player
{
    [SerializeField] private int m_Gold = 15;
    [SerializeField] private int m_Mana = 0;
    public static new TDPlayer Instance
    {
       get
        {
            return Player.Instance as TDPlayer;
        }
    }   
    private void Start()
    { 
        var m_HPlevel = Upgrades.GetUpgradeLevel(healthUpgrade);

        TakeDamage(-m_HPlevel * 5);
    }

    private static event Action<int> OnGoldUpdate;
    public static void GoldUpdateSubscribe(Action<int> act)
    {
        OnGoldUpdate += act;    
        act(Instance.m_Gold);
    }
    public static void GoldUpdateUnSubscribe(Action<int> act)
    {
        OnGoldUpdate -= act;
    }
    private static event Action<int> OnManaUpdate;
    public static void ManaUpdateSubscribe(Action<int> act)
    {
        OnManaUpdate += act;
        act(Instance.m_Mana);
    }
    public static void ManaUpdateUnSubscribe(Action<int> act)
    {
        OnManaUpdate -= act;
    }

    public static event Action<int> OnLifeUpdate;
    public static void LifeUpdateSubscribe(Action<int> act)
    {
        OnLifeUpdate += act;       
        act(Instance.HitPoints);
    }
    [SerializeField] private UpgradeAsset healthUpgrade;   
        
    public void ChangeMana(int change)
    {
        print("Mana was = " + m_Mana);
        m_Mana += change;
        print("Mana become = " + m_Mana);
        OnManaUpdate(m_Mana);
    }

    public void ChangeGold(int change)
    {
        m_Gold += change;        
        OnGoldUpdate(m_Gold);
    }
    public void ReduceLife(int change)
    {
        TakeDamage(change);
        OnLifeUpdate(HitPoints);
    }
    [SerializeField] private Tower m_towerPrefab;
    public void TryBuild(TowerAsset towerAsset, Transform buildSite)
    {
        ChangeGold(-towerAsset.goldCost);        
        var tower = Instantiate(m_towerPrefab, buildSite.position, Quaternion.identity);
        tower.GetComponentInChildren<SpriteRenderer>().sprite = towerAsset.sprite;
        tower.GetComponentInChildren<Turret>().AssignLoadout(towerAsset.turretProperties);
        tower.InitializeTowerDevelopment( towerAsset.m_UpgradesTo);

        Destroy(buildSite.gameObject);
    }
    
    private void OnDestroy()
    {        
        OnGoldUpdate = null;
        OnLifeUpdate = null;
        OnManaUpdate = null;
    }
}
