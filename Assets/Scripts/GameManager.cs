using UnityEngine;
using UnityEngine.SceneManagement;

//하위 매니저들에 대한 접근처를 제공하며, 현재는 게임 오버를 처리하는 싱글톤 클래스
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject gameOverPanel;

    public bool IsGameOver { get; private set; }


    //매니저 클래스들
    //GameManager.Instance.Reward ... 이런 식으로 호출할 수 있도록

    private RewardManager reward;
    private InventoryManager inventory;
    private EquipmentManager equipment;

    public RewardManager Reward => reward;
    public InventoryManager Inventory => inventory;

    public EquipmentManager Equipment => equipment;

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
        //만약 null이라면
        if(reward == null)
        {
            //추가하는데, 만약 실패하면 오류문구 내뱉음
            if(!TryGetComponent<RewardManager>(out reward))
            {
                Debug.Log("[GameManager] : RewardManager가 컴포넌트로 존재하지 않습니다.");               
            }
        }

        if(inventory == null)
        {
            if (!TryGetComponent<InventoryManager>(out inventory))
            {
                Debug.Log("[GameManager] : InventoryManager가 컴포넌트로 존재하지 않습니다.");
            }
        }

        if (equipment == null)
        {
            if (!TryGetComponent<EquipmentManager>(out equipment))
            {
                Debug.Log("[GameManager] : EquipmentManager가 컴포넌트로 존재하지 않습니다.");
            }
        }


    }
}
