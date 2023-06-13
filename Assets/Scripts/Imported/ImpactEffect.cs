using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
    [SerializeField] private float m_ImpactTimer;  
        
    void Update()
    {
        if (m_ImpactTimer > 0) m_ImpactTimer -= Time.deltaTime;

        if (m_ImpactTimer <= 0) Destroy(gameObject);
    }
}
