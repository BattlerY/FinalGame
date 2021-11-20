using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _achievement;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Stronghold _heroesStronghold;
    [SerializeField] private Stronghold _enemiesStronghold;

    private void OnEnable()
    {
        _achievement.text = $"Вы смогли преодолеть волн: {_enemySpawner.CurentWave + 1}";

        if (_enemySpawner.GameFinished)
            _nextLevelButton.gameObject.SetActive(false);
    }

    public void NextLevel()
    {
        _enemySpawner.StartNextVawe();
        gameObject.SetActive(false);
        _heroesStronghold.ResetHealth();
        _enemiesStronghold.ResetHealth();
    }

    public void Quit() => Application.Quit();
}
