using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stronghold : Entity
{
    [SerializeField] private GameObject _endField;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private HeroSpawner _heroSpawner;
    [SerializeField] private Slider _healthBar;

    public override void TakeDamage(int damage, DamageType damageType)
    {
        base.TakeDamage(damage, damageType);
        UpdateHealthBar();
    }

    public override void ResetHealth()
    {
        base.ResetHealth();
        UpdateHealthBar();
    }

    protected override void Destroy()
    {
        _enemySpawner.CleanLevel();
        _heroSpawner.CleanLevel();
        _endField.SetActive(true);
    }

    private void UpdateHealthBar()
    {
        _healthBar.value = HealthRatio;
    }
}
