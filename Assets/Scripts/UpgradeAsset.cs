using UnityEngine;

[CreateAssetMenu]
public sealed class UpgradeAsset : ScriptableObject
{
    public Sprite sprite;
    public int[] costByLevel = { 3 };
}
