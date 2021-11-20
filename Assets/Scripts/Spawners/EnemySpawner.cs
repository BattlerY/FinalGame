﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyWave[] _waves;
    [SerializeField] private Transform[] _spawnPoints;

    private List<Unit> _curentEnemies;

    public int CurentWave { get; private set; } = -1;

    public bool GameFinished => CurentWave >= _waves.Length - 1;

    public event UnityAction<int> WaveDefeated;

    private void Awake()
    {
        StartNextVawe();
    }

    public void CleanLevel()
    {
        foreach (var enemy in _curentEnemies)
            Destroy(enemy.gameObject);

        WaveDefeated(_waves[CurentWave].Reward);
    }

    public void StartNextVawe()
    {
        CurentWave++;
        StartCoroutine(InstantiateEnemy());
    }

    private IEnumerator InstantiateEnemy()
    {
        _waves[CurentWave].Init();
        _curentEnemies = new List<Unit>();

        while (_waves[CurentWave].TryGetEnemy(out Unit enemy))
        {
            yield return new WaitForSeconds(_waves[CurentWave].Delay);
            _curentEnemies.Add(Instantiate(enemy, _spawnPoints[Random.Range(0, _spawnPoints.Length)].position, Quaternion.identity));
        }
    }
}

[System.Serializable]
public class EnemyWave
{
    public Unit[] Enemies;
    public int[] Counts;
    public float Delay;
    public int Reward;

    private List<Unit> _enemiesList;

    public void Init()
    {
        _enemiesList = new List<Unit>();

        for (int i = 0; i < Counts.Length; i++)
        {
            while (Counts[i] > 0)
            {
                _enemiesList.Add(Enemies[i]);
                Counts[i]--;
            }
        }
    }

    public bool TryGetEnemy(out Unit enemy)
    {
        enemy = null;

        if (_enemiesList.Count == 0)
            return false;
         
       int enemyNamber = Random.Range(0, _enemiesList.Count);
       enemy = _enemiesList[enemyNamber];
       _enemiesList.RemoveAt(enemyNamber);

       return true;
    }
}