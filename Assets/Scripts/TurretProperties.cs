using UnityEngine;
using System;
public enum TurretMode
{  
    Primary,
    Secondary
}

[CreateAssetMenu]
public sealed class TurretProperties : ScriptableObject
{
    [SerializeField] private TurretMode m_Mode;
    public TurretMode Mode => m_Mode;

    [SerializeField] private Projectile m_ProjectilePrefab;
    public Projectile ProjectilePrefab => m_ProjectilePrefab;

    [SerializeField] private float m_BasicRateOfFire;
    private float m_CurrentRateOfFire;
    public float RateOfFire => m_CurrentRateOfFire;

    [SerializeField] private int m_EnergyUsage;
    public int EnergyUsage => m_EnergyUsage;

    [SerializeField] private int m_AmmoUsage;
    public int AmmoUsage => m_AmmoUsage;

    [SerializeField] private AudioClip m_LaunchSFX;
    public AudioClip LaunchSFX => m_LaunchSFX;

    public void RaiseTempo(int level, float ratio)
    {
        m_CurrentRateOfFire = m_BasicRateOfFire / (1 + ratio * level);
        Debug.Log("Время перезарядки = " + m_CurrentRateOfFire);
    }
}
