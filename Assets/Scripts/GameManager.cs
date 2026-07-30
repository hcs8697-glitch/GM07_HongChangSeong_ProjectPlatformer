using UnityEngine;
using UnityEngine.SceneManagement;

//새싹반 코드 활용
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject gameOverPanel;

    public bool IsGameOver { get; private set; }

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        IsGameOver = false;
        gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        IsGameOver = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
