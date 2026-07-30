using TMPro;
using UnityEngine;

//플레이어와 적의 상태를 UI에 텍스트로 반영하는 스크립트.
//이 스크립트는 플레이어가 들고 있지 않고, UI Panel이 들고 있게 만들 예정.

public class StateView : MonoBehaviour
{
    [Header("플레이어 상태 체크용")]
    [SerializeField] private TextMeshProUGUI playerStateText;
    [SerializeField] private PlayerController player;
    private SimplePlayerStateMachine playerStateMachine;

    [Header("적 상태 체크용")]
    [SerializeField] private TextMeshProUGUI enemyStateText;
    [SerializeField] private EnemyController enemy;
    private SimpleEnemyStateMachine enemyStateMachine;

    void Start()
    {
        playerStateMachine = player.PlayerStateMachine;
        enemyStateMachine = enemy.EnemyStateMachine;
        playerStateMachine.stateChanged += OnStateChanged;
        enemyStateMachine.enemyStateChanged += OnEnemyStateChanged;
    }

    private void OnStateChanged(IState state)
    {
        if (player == null) return;

        if(playerStateText != null)
        {
            playerStateText.text = "Player : " + state.GetType().Name;
        }

    }

    private void OnEnemyStateChanged(IEnemyState state)
    {
        if (enemy == null) return;

        if (enemyStateText != null)
        {
            enemyStateText.text = "Enemy : " + state.GetType().Name;
        }
    }
}
