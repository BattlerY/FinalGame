using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using IJunior.TypedScenes;

public class Lose : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _achievement;
    [SerializeField] private EnemySpawner _enemySpawner;

    private void Start()
    {
        _achievement.text = $"Вы смогли преодолеть {_enemySpawner.CurentWave} волн";
    }

    public void Restart() => Game.Load();
    
    public void Quit() =>Application.Quit();  
}
