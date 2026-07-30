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

    private void Awake() //이벤트 구독
    {
        player.playerHealthChanged += RefreshHealthText;
        enemy.enemyHealthChanged += RefreshEnemyHealthText;
    }

    private void Start() //Start에서 한 번 실행하여 체력을 반영한다.
    {
        RefreshHealthText();
        RefreshEnemyHealthText();
    }

    private void RefreshHealthText()
    {
        if (player == null) return;

        playerHealthText.text = "Player : " + player.CurrentHp;
    }

    private void RefreshEnemyHealthText()
    {
        if (enemy == null) return;

        enemyHealthText.text = "Enemy : " + enemy.CurrentHp;
    }


    private void OnDestroy() //이벤트 구독 해제
    {
        player.playerHealthChanged -= RefreshHealthText;
    }
}
