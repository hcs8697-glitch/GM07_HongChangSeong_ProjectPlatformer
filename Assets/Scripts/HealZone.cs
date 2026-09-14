using UnityEngine;

//OnTriggerStay2D를 활용해, 플레이어가 진입해있는 동안 Heal메서드를 호출할 스크립트.

public class HealZone : MonoBehaviour
{
    [Header("회복량")]
    [SerializeField] private int healAmount = 1;




    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Player태그인 것이 영역 안으로 들어옴");
            PlayerHealth player = collision.GetComponent<PlayerHealth>();

            player.Heal(healAmount);
        }
    }

}
