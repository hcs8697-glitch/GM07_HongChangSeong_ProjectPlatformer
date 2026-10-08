using UnityEngine;
using UnityEngine.SceneManagement;

//새싹반 코드 활용
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject gameOverPanel;

    public bool IsGameOver { get; private set; }


    //매니저 클래스들
    //GameManager.Instance.Reward ... 이런 식으로 호출할 수 있도록

    private RewardManager reward;

    public RewardManager Reward => reward;



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

        EnsureComponents();
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

    private void EnsureComponents()
    {
        if(reward == null)
        {
            if(!TryGetComponent<RewardManager>(out reward))
            {
                Debug.Log("[GameManager] : RewardManager가 컴포넌트로 존재하지 않습니다.");                
            }
        }
    }
}
