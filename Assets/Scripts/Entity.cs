using UnityEngine;
using UnityEngine.Events;

public enum Factions
{
    Hero,
    Enemy
}

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _armor;
    [SerializeField] private Factions _faction;

    private int _curentHealth;

    public Factions Faction => _faction;
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

    public virtual void TakeDamage(int damage, bool isMage=false)
    {
        if (isMage==false) 
            damage = Mathf.Max(damage - _armor, 1);

        _curentHealth -= damage;

        if (IsDead)
            Destroy();
    }

    protected void UpArmor(int armor) => _armor += armor;
    
    protected abstract void Destroy();
}
