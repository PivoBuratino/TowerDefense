using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float m_Velocity;

    [SerializeField] private float m_Lifetime;

    [SerializeField] private int m_Damage;

    [SerializeField] private ImpactEffect m_ImpactEffectPrefab;

    private float m_Timer;

    private void Update()
    {
        float stepLength = Time.deltaTime * m_Velocity;
        Vector2 step = transform.up * stepLength;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

        if (hit)
        {
            Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();           

            if (dest != null && dest != m_Parent)
            {
                dest.ApplyDamage(m_Damage);

                if (Player.Instance.AllParentShips.Contains(m_Parent) && dest.HitPoints <= 0)                
                {
                    Debug.Log("������ ���������!");

                    Player.Instance.AddScore(dest.ScoreValue);

                    if (dest.GetComponent<SpaceShip>() != null) Player.Instance.AddKill();
                }
            }
            OnProjectileLifeEnd(hit.collider, hit.point);
        }
        m_Timer += Time.deltaTime;

        if (m_Timer > m_Lifetime) Destroy(gameObject);

        transform.position += new Vector3(step.x, step.y, 0);
    }
    private void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
    {
        Destroy(gameObject);
    }

    private Destructible m_Parent;

    public void SetParentShooter(Destructible parent)
    {
        m_Parent = parent;
    }
    public void SetTarget(Destructible target)
    {
        
    }
}