using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif 


[RequireComponent(typeof(TDPatrolController))]
[RequireComponent(typeof(Destructible))]
public class Enemy : MonoBehaviour
{
    public enum ArmorType { Base = 0, Mage = 1, Fire = 2 }
    private static Func<int, TDProjectile.DamageType, int, int>[] ArmorDamageFunctions =
     {
        (int damage, TDProjectile.DamageType type, int armor) =>
        { // ArmorType.Base
            switch (type)
            {
                case TDProjectile.DamageType.Magic: return damage;
                case TDProjectile.DamageType.Fire: return damage;
                    default: return Mathf.Max(damage - armor, 3);
            }
        },
        (int damage, TDProjectile.DamageType type, int armor) =>
        { // ArmorType.Magic
            switch (type)
            {
                case TDProjectile.DamageType.Fire: return damage;
                case TDProjectile.DamageType.Base: return Mathf.Max(damage - armor/2, 3);
                    default: return Mathf.Max(damage - armor, 3);
            }
        },
        (int damage, TDProjectile.DamageType type, int armor) =>
        { // ArmorType.Fire
            switch (type)
            {
                case TDProjectile.DamageType.Base: return damage;
                case TDProjectile.DamageType.Magic: return damage;
                     default: return Mathf.Max(damage - armor, 5);
            }
        }
    };

    [SerializeField] private int m_damage = 1;
    [SerializeField] private int m_gold = 1;
    [SerializeField] private int m_mana = 0;
    [SerializeField] private int m_armor = 1;
    [SerializeField] private ArmorType m_ArmorType;

    private Destructible m_destructible;

    public event Action OnEnd;

    private void Awake()
    {
        m_destructible = GetComponent<Destructible>();
    }
    private void OnDestroy()
    {
        OnEnd?.Invoke();
    }

    public void Use(EnemyAsset asset)
    {
        var sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        sr.color = asset.color;
        sr.transform.localScale = new Vector3(asset.spriteScale.x, asset.spriteScale.y, 1);

        sr.GetComponent<Animator>().runtimeAnimatorController = asset.animations;
        GetComponent<SpaceShip>().Use(asset);
        GetComponentInChildren<CircleCollider2D>().radius = asset.radius;
        m_armor = asset.armor;
        m_ArmorType = asset.armorType;
        m_damage = asset.damage;
        m_mana = asset.mana;
        m_gold = asset.gold;
    }
    public void DamagePlayer()
    {
        (Player.Instance as TDPlayer).ReduceLife(m_damage);
    }
    public void GivePlayerGold()
    {
        (Player.Instance as TDPlayer).ChangeGold(m_gold);
    }
    public void GivePlayerMana()
    {
        (Player.Instance as TDPlayer).ChangeMana(m_mana);
    }
    public void TakeDamage(int damage, TDProjectile.DamageType damageType)
    {
        m_destructible.ApplyDamage(ArmorDamageFunctions[(int)m_ArmorType](damage, damageType, m_armor));

    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(Enemy))]
public class EnemyInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EnemyAsset a = EditorGUILayout.ObjectField(null, typeof(EnemyAsset), false) as EnemyAsset;
        if (a)
        {
            (target as Enemy).Use(a);
        }
    }
}
#endif