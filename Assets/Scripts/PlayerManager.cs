using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private List<Mob> _units;
    [SerializeField] private List<TextMeshProUGUI> _costTexts;
    [SerializeField] private List<Button> _buyButtons;

    private int _money = 100;

    public int CurentUnitNumber { get; private set; } = -1;

    public bool TryBuy(out Mob unit)
    {
        unit = null;
        if (CurentUnitNumber < 0)
            return false;

        _money -= _units[CurentUnitNumber].Cost;
        int temp = CurentUnitNumber;

        CheckSolvency(CurentUnitNumber);
        _moneyText.text = _money.ToString();

        unit = _units[temp];
        return true;
    }

    public void AddMoney(int money)
    {
        _money += money;
        _moneyText.text = _money.ToString();
    }

    private void Awake()
    {
        _moneyText.text = _money.ToString();

        for (int i = 0; i < _units.Count; i++)
        {
            _costTexts[i].text = _units[i].Cost.ToString();
            int temp = i;
            _buyButtons[i].onClick.AddListener(() => CheckSolvency(temp));
        }
    }

    private void OnEnable()
    {
        Mob.Dying += AddMoney;
    }

    private void OnDisable()
    {
        Mob.Dying -= AddMoney;
    }

    private void CheckSolvency(int unit)
    {
        if (_units[unit].Cost <= _money)
            CurentUnitNumber = unit;
        else if (unit == CurentUnitNumber)
            CurentUnitNumber = -1;
    }
}
