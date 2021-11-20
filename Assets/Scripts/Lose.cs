using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _achievement;
    [SerializeField] private EnemySpawner _enemySpawner;

    private void Start()
    {
        _achievement.text = $"Вы смогли преодолеть {_enemySpawner.CurentWave} волн";
    }

    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    
    public void Quit() =>Application.Quit();  
}
