using UnityEngine;

public class EntitySpawner : Spawner
{
    protected override GameObject GenerateSpawnedEntity()
    {
        return Instantiate(m_EntityPrefabs[Random.Range(0, m_EntityPrefabs.Length)]);    
    }

    [SerializeField] private GameObject[] m_EntityPrefabs;  
    
}
