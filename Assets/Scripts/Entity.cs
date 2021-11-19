using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _armor;
    [SerializeField] private Factions _faction;

    private int _curentHealth;

    public Factions Faction => _faction;
    public bool IsDead => _curentHealth <= 0;
    public float HealthRatio => 1f * _curentHealth / _maxHealth;

    public event UnityAction HealthChanged;

    private void Start()
    {
        _curentHealth = _maxHealth;
    }

    public void TakeDamage(int damage, bool isMage=false)
    {
        if (isMage==false) 
            damage = Mathf.Max(damage - _armor, 1);

        _curentHealth -= damage;

        if(this is Stronghold)
             HealthChanged();

        if (IsDead)
            OnDying();
    }

    public void UpArmor(int armor) => _armor += armor;
    
    public abstract void OnDying();
}
