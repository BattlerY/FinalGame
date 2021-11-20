using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private List<Unit> _heroes;
    [SerializeField] private List<TextMeshProUGUI> _costTexts;
    [SerializeField] private List<Button> _buyButtons;
    [SerializeField] private int _startMoney;
    [SerializeField] private EnemySpawner _enemySpawner;


    private int _money;
    private int _curentHeroNumber = -1;

    private void Awake()
    {
        _money = _startMoney;
        _moneyText.text = _money.ToString(); ;

        for (int i = 0; i < _heroes.Count; i++)
        {
            _costTexts[i].text = _heroes[i].Cost.ToString();
            int temp = i;
            _buyButtons[i].onClick.AddListener(() => CheckSolvency(temp));
        } 
    }

    private void OnEnable()
    {
        Unit.EnemyDied += AddMoney;
        _enemySpawner.WaveDefeated += AddMoney;
    }

    private void OnDisable()
    {
        Unit.EnemyDied -= AddMoney;
        _enemySpawner.WaveDefeated -= AddMoney;
    }

    public bool TryBuy(out Unit hero)
    {
        hero = null;

        if (_curentHeroNumber < 0)
            return false;

        _money -= _heroes[_curentHeroNumber].Cost; 
        _moneyText.text = _money.ToString();
        hero = _heroes[_curentHeroNumber];
        CheckSolvency(_curentHeroNumber);

        return true;
    }

    private void AddMoney(int money)
    {
        _money += money;
        _moneyText.text = _money.ToString();
    }

    private void CheckSolvency(int heroNumber)
    {
        if (_heroes[heroNumber].Cost <= _money)
            _curentHeroNumber = heroNumber;
        else if (heroNumber == _curentHeroNumber)
            _curentHeroNumber = -1;
    }
}
