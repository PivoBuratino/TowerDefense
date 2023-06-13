using UnityEngine;
using System.Collections.Generic;
using System;

public class Player : SingletonBase<Player>
{
    [SerializeField] private int m_HitPoints;
    public int HitPoints => m_HitPoints;
    [SerializeField] private SpaceShip m_Ship;
    [SerializeField] private GameObject m_PlayerShipPrefab;    
    public SpaceShip ActiveShip => m_Ship;

    //[SerializeField] private CameraController m_CameraController;
    //[SerializeField] private MovementController m_MovementController;    

    public List<Destructible> AllParentShips;

    protected override void Awake()
    {
        base.Awake();

        if (m_Ship != null) Destroy(m_Ship.gameObject);
    }

    private void Start()
    {
        if (m_Ship) m_Ship.EventOnDeath.AddListener(OnShipDeath);
        Respawn(); 
    }

    private void OnShipDeath()
    {
        if (m_HitPoints > 0)
        {
            m_HitPoints--;           

            Respawn();
        }
        else
        { 
            //LevelSequenceController.Instance.FinishCurrentLevel(false);
        }        
    }    

    internal void TakeDamage(int m_damage)
    {
        m_HitPoints -= m_damage;
        if (m_HitPoints <= 0)
        {
            //Time.timeScale = 0;
            //print("Game Over");
            //LevelSequenceController.Instance.FinishCurrentLevel(false);
            LevelSequenceController.Instance.RestartLevel();
        }
    }

    private void Respawn()
    {
        if (LevelSequenceController.PlayerShip != null)
        {
            var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip);

            m_Ship = newPlayerShip.GetComponent<SpaceShip>();
            m_Ship.name = "Player Ship";           
            
            AllParentShips.Add(m_Ship);
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
