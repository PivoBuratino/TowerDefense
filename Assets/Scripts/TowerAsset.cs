using UnityEngine;


[CreateAssetMenu]
public class TowerAsset : ScriptableObject
{
    public int goldCost = 15;
    public Sprite sprite;
    public Sprite GUISprite;
    public TurretProperties turretProperties;

    /*
    public void Build(Vector2 position, Tower towerPrefab)
    {
        var tower = Instantiate(towerPrefab, position, Quaternion.identity);
        tower.GetComponent<SpriteRenderer>().sprite = sprite; 
    }
    */
}

