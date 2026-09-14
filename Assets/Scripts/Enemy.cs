using System;
using UnityEngine;

//TODO
//플레이어와 똑같이 현재 체력 관리하게 하기.

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("최대 체력")]
    [SerializeField] private int maxHp = 100;
    [Header("현재 체력")]
    [SerializeField] private int currentHp;
    [Header("공격력")]
    [SerializeField] private int attackPower = 5;
    [Header("드랍 아이템")] //배열로 만들면, 여러 아이템들을 생성하게 하거나 랜덤으로 둘 중 하나를 생성하게 할 수 있다.
    [SerializeField] private GameObject dropItem;

    public int CurrentHp => currentHp;

    //플레이어의 체력이 변화할 때 발행할 이벤트.
    public event Action enemyHealthChanged;


    private void Awake()
    {
        currentHp = maxHp;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어 태그인 것과 접촉 성공");
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            player.TakeDamage(attackPower);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        Debug.Log($"적이 피해를 입음. 피해 : {damage}, 현재 적의 체력 : {currentHp}");
        enemyHealthChanged?.Invoke();


        if (currentHp <= 0)
        {
            Debug.Log("적 사망");
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (currentHp + amount > maxHp) return;

        currentHp += amount;
        enemyHealthChanged?.Invoke();
    }

    private void Die()
    {
        //죽었을 때 설정한 드랍 아이템을 자신의 위치에다 생성한다.

        if(dropItem != null)
        {
            Instantiate(dropItem, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
