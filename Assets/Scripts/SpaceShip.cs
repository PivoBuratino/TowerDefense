using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceShip : Destructible
{
    /// <summary>
    /// 
    /// </summary>
    [Header("Space Ship")]
    [SerializeField] private float m_Mass;
    /// <summary>
    /// Forward moving force
    /// </summary>
    [SerializeField] private float m_Thrust;
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private float m_Mobility;


    /// <summary>
    /// Max linear velocity
    /// </summary>
    [SerializeField] private float m_MaxLinearVelocity;
    public float MaxLinearVelocity => m_MaxLinearVelocity;
    private float m_MaxVelocityBackUp;
    public void HalfMaxLinearVelocity() 
    {
        m_MaxVelocityBackUp = m_MaxLinearVelocity;
        m_MaxLinearVelocity /= 2; 
    }
    public void RestoreMaxLinearVelocity() { m_MaxLinearVelocity = m_MaxVelocityBackUp; }

    [SerializeField] private float m_MaxAngularVelocity;
    public float MaxAngularVelocity => m_MaxAngularVelocity;

    [SerializeField] private Sprite m_PreviewImage;
    public Sprite PreviewImage => m_PreviewImage;
    /// <summary>
    /// 
    /// </summary>
    private Rigidbody2D m_Rigid;

    #region Public API

    /// <summary>
    /// 
    /// </summary>
    public float ThrustControl { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public float TorqueControl { get; set; }

    #endregion

    #region Unity Event
    protected override void Start()
    {
        base.Start();

        m_Rigid = GetComponent<Rigidbody2D>();
        m_Rigid.mass = m_Mass;

        m_Rigid.inertia = 1;

        //InitOffensive();
    }

    private void FixedUpdate()
    {
        UpdateRigidBody();

        //UpdateEnergyRegen();
    }

    #endregion

    /// <summary>
    /// Add force to move ship
    /// </summary>
    private void UpdateRigidBody()
    {
        m_Rigid.AddForce(m_Thrust * ThrustControl * Time.fixedDeltaTime * transform.up, ForceMode2D.Force);

        m_Rigid.AddForce((m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime * -m_Rigid.velocity, ForceMode2D.Force);

        m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);

        m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
    }

    /*
    [SerializeField] private Turret[] m_Turrets;

   

    [SerializeField] private int m_MaxEnergy;
    [SerializeField] private int m_MaxAmmo;
    [SerializeField] private int m_EnergyRegenPerSecond;

    private float m_PrimaryEnergy;
    private int m_SecondaryAmmo;

    public void AddEnergy(int e)
    {
        m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + e, 0, m_MaxEnergy);
    }

    public void AddAmmo(int ammo)
    {
        m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
    }
    private void InitOffensive()
    {
        m_PrimaryEnergy = m_MaxEnergy;
        m_SecondaryAmmo = m_MaxAmmo;
    }

    private void UpdateEnergyRegen()
    {
        m_PrimaryEnergy += (float)m_EnergyRegenPerSecond * Time.fixedDeltaTime;
        m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_MaxEnergy);
    }

    public void AssignWeapon(TurretProperties props)
    {
        for (int i = 0; i < m_Turrets.Length; i++)
        {
            m_Turrets[i].AssignLoadout(props);
        }
    }
    */

    public void Fire(TurretMode mode)
    {
        return;
    }

    public bool DrawEnergy(int count)
    {
        return true;
    }

    public bool DrawAmmo(int count)
    {
      return true;
    }  

    public void AccelerateShip(float acceleration, float timer)
    {

    }
    public override void Use(EnemyAsset asset)
    {
        base.Use(asset);
        m_MaxLinearVelocity = asset.moveSpeed;
    }
}
