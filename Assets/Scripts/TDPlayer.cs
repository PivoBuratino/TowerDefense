using UnityEngine;
using System;

public class TDPlayer : Player
{
    [SerializeField] private int m_gold = 0;
    public static new TDPlayer Instance
    {
        get
        {
            return Player.Instance as TDPlayer;
        }
    }

    private static event Action<int> OnGoldUpdate;
    public static void GoldUpdateSubscribe(Action<int> act)
    {
        OnGoldUpdate += act;
        act(Instance.m_gold);
    }
    private static event Action<int> OnLifeUpdate;
    public static void LifeUpdateSubscribe(Action<int> act)
    {
        OnLifeUpdate += act;
        act(Instance.HitPoints);
    }  

    private void Start()
    {        
        OnLifeUpdate(HitPoints);
        OnGoldUpdate(m_gold);
    }
    public void ChangeGold(int change)
    {
        m_gold += change;
        OnGoldUpdate(m_gold);
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
        Destroy(buildSite.gameObject);
    }
}