using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyVawe[] _vawes;
    [SerializeField] private Transform[] _spawnPoints;

    private int curentWave = -1;
    private List<Mob> _curentEnemies;

    private void Awake()
    {
        StartNextVawe();
    }

    public void CleanLevel()
    {
        foreach (var enemy in _curentEnemies)
            Destroy(enemy.gameObject);
    }

    private void StartNextVawe()
    {
        curentWave++;
        StartCoroutine(InstantiateEnemy());
    }

    private IEnumerator InstantiateEnemy()
    {
        _vawes[curentWave].Init();
        _curentEnemies = new List<Mob>();

        while (_vawes[curentWave].TryGetEnemy(out Mob enemy))
        {
            yield return new WaitForSeconds(_vawes[curentWave].Delay);
            _curentEnemies.Add(Instantiate(enemy, _spawnPoints[Random.Range(0, _spawnPoints.Length)].position, Quaternion.identity));
        }
    }
}

[System.Serializable]
public class EnemyVawe
{
    public Mob[] Mobs;
    public int[] Counts;
    public float Delay;

    private List<Mob> _mobsList;

    public void Init()
    {
        _mobsList = new List<Mob>();

        for (int i = 0; i < Counts.Length; i++)
        {
            while (Counts[i] > 0)
            {
                _mobsList.Add(Mobs[i]);
                Counts[i]--;
            }
        }
    }
    public bool TryGetEnemy(out Mob enemy)
    {
        enemy = null;

        if (_mobsList.Count == 0)
            return false;
         
       int enemyNamber = Random.Range(0, _mobsList.Count);
       enemy = _mobsList[enemyNamber];
       _mobsList.RemoveAt(enemyNamber);

       return true;
    }
}