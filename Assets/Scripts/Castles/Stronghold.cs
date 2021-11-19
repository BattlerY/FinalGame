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

    private void OnEnable()
    {
        HealthChanged += OnHealthChanged;
    }
    private void OnDisable()
    {
        HealthChanged -= OnHealthChanged;
    }

    public override void OnDying()
    {
        _enemySpawner.CleanLevel();
        _heroSpawner.CleanLevel();
        _endField.SetActive(true);
    }

    private void OnHealthChanged()
    {
        _healthBar.value = HealthRatio;
    }
}
