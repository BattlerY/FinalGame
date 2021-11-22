using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroSpawner : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    private Button[] _spawnPoints;
    private List<Unit> _curentHeroes;

    private void Awake()
    {
        _spawnPoints = new Button[transform.childCount];
        _curentHeroes = new List<Unit>();

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _spawnPoints[i] = transform.GetChild(i).GetComponent<Button>();
            int temp = i;
            _spawnPoints[i].onClick.AddListener(() => CreateUnit(temp));
        }
    }

    public void CleanLevel()
    {
        foreach (var unit in _curentHeroes)
            Destroy(unit.gameObject);
        _curentHeroes = new List<Unit>();
    }

    private void CreateUnit(int heroNumber)
    {
        if(_gameManager.TryBuy(out Unit hero))
            _curentHeroes.Add(Instantiate(hero, _spawnPoints[heroNumber].transform.position, Quaternion.identity));
    }
}
