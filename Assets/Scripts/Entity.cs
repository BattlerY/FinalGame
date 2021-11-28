using UnityEngine;
using UnityEngine.Events;

public enum Faction
{
    Hero,
    Enemy
}

public enum DamageType
{
    Magical,
    Physical
}

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _armor;
    [SerializeField] private Faction _faction;

    private int _curentHealth;

    public Faction Faction => _faction;
    public bool IsDead => _curentHealth <= 0;
    public float HealthRatio => 1f * _curentHealth / _maxHealth;

    private void Start()
    {
        ResetHealth();
    }

    public virtual void ResetHealth()
    {
        _curentHealth = _maxHealth;
    }

    public virtual void TakeDamage(int damage, DamageType damageType)
    {
        if (damageType == DamageType.Physical) 
            damage = Mathf.Max(damage - _armor, 1);

        _curentHealth -= damage;

        if (IsDead)
            Destroy();
    }

    protected void UpArmor(int armor) => _armor += armor;
    
    protected abstract void Destroy();
}
