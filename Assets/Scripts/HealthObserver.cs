using TMPro;
using UnityEngine;

//체력이 변화하는 이벤트를 구독하고, 그걸 Text에 반영하게 할 스크립트.

public class HealthObserver : MonoBehaviour
{
    [Header("플레이어")]
    [SerializeField] private TextMeshProUGUI playerHealthText;
    [SerializeField] private Player player;
    [Header("적")]
    [SerializeField] private TextMeshProUGUI enemyHealthText;
    [SerializeField] private Enemy enemy;

    private void Awake() 
    {

    }

    private void Start() //이벤트부터 구독 후 Start에서 한 번 실행하여 체력을 반영한다. 그리고 이거... 방어코드 있어야 할 거 같은데
    {
        player.Health.playerHealthChanged += RefreshHealthText;
        enemy.enemyHealthChanged += RefreshEnemyHealthText;


        RefreshHealthText();
        RefreshEnemyHealthText();
    }

    private void RefreshHealthText()
    {
        if (player == null) return;

        playerHealthText.text = "Player : " + player.Health.CurrentHp;
    }

    private void RefreshEnemyHealthText()
    {
        if (enemy == null) return;

        enemyHealthText.text = "Enemy : " + enemy.CurrentHp;
    }


    private void OnDestroy() //이벤트 구독 해제
    {
        player.Health.playerHealthChanged -= RefreshHealthText;
    }
}
