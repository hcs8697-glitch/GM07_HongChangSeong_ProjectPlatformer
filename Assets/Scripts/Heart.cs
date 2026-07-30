using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private int healAmount = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();

            //만약 현재 체력과 최대 체력이 같은 상태라면 실행하지 않는다.
            if (player.CurrentHp == player.MaxHp) return;

            player.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
