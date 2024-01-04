using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Abilities : SingletonBase<Abilities>
{
    [Serializable]
    public class FireAbility 
    {
        [SerializeField] private float m_Radius = 5f;
        public float Radius => m_Radius;

        [SerializeField] private int m_Damage = 2;
        [SerializeField] private int m_Cost = 5;
        public int Cost => m_Cost;
        [SerializeField] private Color m_TargetingColor;

        
        public void Use()
        {
            ClickProtection.Instance.Activate((Vector2 v) =>
            {
                Vector3 position = v;
                position.z = -Camera.main.transform.position.z;
                position = Camera.main.ScreenToWorldPoint(position);
                foreach (var collider in Physics2D.OverlapCircleAll(position, m_Radius))
                {
                    if (collider.transform.parent.TryGetComponent<Enemy>(out var enemy))
                    {
                        enemy.TakeDamage(m_Damage, TDProjectile.DamageType.Fire);
                    }
                }
                TDPlayer.Instance.ChangeMana(-m_Cost);
            });
        }
        public void IncreaseRadius()
        {
            m_Radius *= 2;
        }
    }

    [Serializable]
    public class TimeAbility 
    {
        [SerializeField] private float m_Cooldown = 15f;
        [SerializeField] private float m_Duration = 5f;
        [SerializeField] private float m_DurationLevelUp;
        private float m_Timer;
        [SerializeField] private TMP_Text killSpellClock;
        private bool activated = false;        
        
        public void RefreshSlowSpellTimer()
        {
            if (activated == true && m_Timer <= 0)
                {
                    m_Timer = m_Cooldown - m_Duration;
                    killSpellClock.color = Color.red;
                    activated = false;
                }
            
            if (m_Timer > 0) m_Timer -= Time.deltaTime;
            killSpellClock.text = ((int)m_Timer).ToString();
        }
        public void SetKillSpellTimer()
        {
            m_Timer = m_Duration;
            killSpellClock.color = Color.white;
            activated = true;
        }
        public void Use()
        {            
            SetKillSpellTimer();
            void Slow(Enemy ship)
            {
                ship.GetComponent<SpaceShip>().HalfMaxLinearVelocity();                
            }
            IEnumerator Restore()
            {
                yield return new WaitForSeconds(m_Duration);
                foreach (var ship in FindObjectsOfType<SpaceShip>())
                    ship.RestoreMaxLinearVelocity();
                EnemyWaveManager.OnEnemySpawn -= Slow;
            }
            foreach (var ship in FindObjectsOfType<SpaceShip>())
            {
                ship.HalfMaxLinearVelocity();
            }
            EnemyWaveManager.OnEnemySpawn += Slow;
            Instance.StartCoroutine(Restore());
            
            IEnumerator TimeAbilityButton()
            {
                Instance.SlowButton.interactable = false;                
                yield return new WaitForSeconds(m_Cooldown);                
                Instance.SlowButton.interactable = true;
            }
            Instance.StartCoroutine(TimeAbilityButton());            
        }
        public void SetDuration()
        {
            m_Duration *= m_DurationLevelUp;
        }
    }

    [SerializeField] private Button SlowButton;
    [SerializeField] private Button KillButton;
    [SerializeField] private FireAbility m_FireAbility;
    public void UseFireAbility() => m_FireAbility.Use();
    public float KillRadius => m_FireAbility.Radius;
    [SerializeField] private TimeAbility m_TimeAbility;
    public void UseTimeAbility() => m_TimeAbility.Use();

    [SerializeField] private UpgradeAsset killUpgrade;
    private int m_KillLevel;   

    [SerializeField] private UpgradeAsset slowUpgrade;
    private int m_SlowLevel;
    [SerializeField] private TMP_Text m_ManaCostText;

    private void Update()
    {
        m_TimeAbility.RefreshSlowSpellTimer();
    }
    private void Start()
    {        
        //инициализация ручной бомбы
        m_KillLevel = Upgrades.GetUpgradeLevel(killUpgrade);
        
        if (m_KillLevel >= 2)
        {            
            m_FireAbility.IncreaseRadius();           
        }

        //инициализация замедления врагов
        m_SlowLevel = Upgrades.GetUpgradeLevel(slowUpgrade);
        if (m_SlowLevel < 1) { SlowButton.interactable = false; }
        if (m_SlowLevel >= 1) 
        {
            m_TimeAbility.SetDuration();
        }

        m_ManaCostText.text = m_FireAbility.Cost.ToString();
        TDPlayer.ManaUpdateSubscribe(ManaStatusCheck);
    }    
    private void ManaStatusCheck(int mana)
    {
        if (mana >= 5 && m_KillLevel >= 1)
        {
            KillButton.interactable = true;
            m_ManaCostText.color = Color.white;
        }
        else
        {
            KillButton.interactable = false;
            m_ManaCostText.color = Color.red;
        }
    }
    private void OnDestroy()
    {
        TDPlayer.ManaUpdateUnSubscribe(ManaStatusCheck);
    }
}
