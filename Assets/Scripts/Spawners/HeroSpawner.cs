using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroSpawner : MonoBehaviour
{
    [SerializeField] PlayerManager _playerManager;

    private Button[] _spawnPoints;
    private List<Mob> _curentUnits;

    private void Awake()
    {
        _spawnPoints = new Button[transform.childCount];
        _curentUnits = new List<Mob>();

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _spawnPoints[i] = transform.GetChild(i).GetComponent<Button>();
            int temp = i;
            _spawnPoints[i].onClick.AddListener(() => CreateUnit(temp));
        }
    }
    public void CleanLevel()
    {
        foreach (var unit in _curentUnits)
            Destroy(unit.gameObject);
        _curentUnits = new List<Mob>();
    }
    private void CreateUnit(int unitNumber)
    {
        if(_playerManager.TryBuy(out Mob unit))
            _curentUnits.Add(Instantiate(unit, _spawnPoints[unitNumber].transform.position, Quaternion.identity));
    }

}
