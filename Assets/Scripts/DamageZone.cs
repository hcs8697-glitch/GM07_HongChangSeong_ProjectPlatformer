using UnityEngine;

//OnTriggerStay2D를 활용해, 플레이어가 진입해있는 동안 Heal메서드를 호출할 스크립트.

public class DamageZone : MonoBehaviour
{
    [Header("피해량")]
    [SerializeField] private int damage = 1;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player태그인 것이 영역 안으로 들어옴");
            Player player = collision.GetComponent<Player>();

            player.TakeDamage(damage);
        }
    }
}
