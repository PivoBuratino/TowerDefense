using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Уничтожаемый объект на сцене. То, что может иметь хитпойнты
/// </summary>
public class Destructible : Entity
{
    #region Properties

    /// <summary>
    /// Объект игнорирует повреждения
    /// </summary>
    [SerializeField] private bool m_Indestructible;
    public bool IsIndestructible => m_Indestructible;

    /// <summary>
    /// Стартовое количество хитпойнтов
    /// </summary>
    [SerializeField] private int m_HitPoints;

    /// <summary>
    /// Текущие хитпойнты
    /// </summary>
    private int m_CurrentHitPoints;
    public int HitPoints => m_CurrentHitPoints;

    [SerializeField] ImpactEffect effectOnDeath;

    #endregion

    #region Unity Events
    
    protected virtual void Start()
    {
        m_CurrentHitPoints = m_HitPoints;              
    }

    #endregion

    #region Public API

    /// <summary>
    /// Нанесение повреждений объекту
    /// </summary>
    /// <param name="damage">Величина нанесенного урона</param>
    public void ApplyDamage(int damage)
    {
        if (m_Indestructible) return;

        m_CurrentHitPoints -= damage;

        if(m_CurrentHitPoints <= 0)
        {
            OnDeath();
        }
    }

    #endregion
    /// <summary>
    /// Переопределяемое событие когда хитпойнт ниже нуля
    /// </summary>
    [SerializeField] private UnityEvent m_EventOnDeath;
    public UnityEvent EventOnDeath => m_EventOnDeath;
    protected virtual void OnDeath()
    {
        Destroy(gameObject);

        m_EventOnDeath?.Invoke();

        Instantiate(effectOnDeath, transform.position, transform.rotation);
    }

    #region Teams
    private static HashSet<Destructible> m_AllDestructibles;
    public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;
    protected virtual void OnEnable()
    {
        if (m_AllDestructibles == null) m_AllDestructibles = new HashSet<Destructible>();

        m_AllDestructibles.Add(this);
    }
    protected virtual void OnDestroy()
    {
        m_AllDestructibles.Remove(this);
        m_EventOnDeath = null;
    }

    public const int TeamIdNeutral = 0;

    [SerializeField] private int m_TeamId;
    public int TeamId => m_TeamId;
    #endregion

    #region Score

    [SerializeField] private int m_ScoreValue;
    public int ScoreValue => m_ScoreValue;

    #endregion

    public virtual void Use(EnemyAsset asset)
    {
        m_HitPoints = asset.hp;       
        m_ScoreValue = asset.score;
    }
    
}
