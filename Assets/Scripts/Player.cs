using UnityEngine;
using System.Collections.Generic;
using System;

public class Player : SingletonBase<Player>
{
    public event Action OnPlayerDead; 

    [SerializeField] private int m_HitPoints;
    public int HitPoints => m_HitPoints;  

    private int m_Lives = 0;

    protected override void Awake()
    {
        base.Awake();

        //if (m_Ship != null) Destroy(m_Ship.gameObject);
    }

    private void Start()
    {
        //if (m_Ship) m_Ship.EventOnDeath.AddListener(OnShipDeath);
        Respawn(); 
    }
    private void OnDestroy()
    {
        //if (m_Ship) m_Ship.EventOnDeath.RemoveListener(OnShipDeath);
    }

    private void OnShipDeath()
    {
        if (m_Lives > 0)
        {
            m_Lives--;           

            Respawn();
        }
        else
        { 
            LevelSequenceController.Instance.FinishCurrentLevel(false);
        }        
    }    

    public void TakeDamage(int m_damage)
    {
        m_HitPoints -= m_damage;
        if (m_HitPoints <= 0)
        {
            m_HitPoints = 0;
            OnPlayerDead?.Invoke();          
        }
    }

    private void Respawn()
    {
        if (LevelSequenceController.PlayerShipPrefab != null)
        {
            var newPlayerShip = Instantiate(LevelSequenceController.PlayerShipPrefab);
        }
    }
    #region score

    public int Score { get; private set; }

    public int NumKills { get; private set; }

    public void AddKill()
    {
        NumKills++;
    }

    public void AddScore(int num)
    {
        Score += num;
    }

    #endregion
}
