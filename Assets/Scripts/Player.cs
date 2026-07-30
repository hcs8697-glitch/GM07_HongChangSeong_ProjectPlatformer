using System;
using UnityEngine;

//플레이어의 스탯과 체력 등등을 담당할 스크립트.
//나중에는 PlayerController, GrounderChecker등도 필드로 갖게 하고,
//이 클래스만 StateMachine을 생성할 때 전달하는 식으로 바꿔야 한다.
//추후에 다양한 캐릭터를 구현하고자 한다면, SO를 만들어 할당하는 식으로 작업할 수 있을 것.


public class Player : MonoBehaviour, IDamageable
{
    [Header("최대 체력")]
    [SerializeField] private int maxHp = 100;
    [Header("현재 체력")]
    private int currentHp;


    //플레이어의 체력이 변화할 때 발행할 이벤트.
    public event Action playerHealthChanged;


    public int CurrentHp
    {
        get => currentHp;

        private set //현재 체력이 변경될 때, 여기에서 체력이 변경되었음을 알리는 이벤트를 Invoke한다.
        { //다만, 현 상황에서는 체력이 변경되지 않음에도 Invoke가 실행될 수 있다.
            currentHp = Mathf.Clamp(value, 0, maxHp);

            playerHealthChanged?.Invoke();

            if (currentHp <= 0)
            {
                Die();
            }
        }
    }
    public int MaxHp => maxHp;



    private void Awake()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        CurrentHp -= damage;
        /*
        playerHealthChanged?.Invoke();

        if(currentHp <= 0)
        {
            Die();
        }
        */
    }

    public void Heal(int amount)
    {        
        CurrentHp += amount;
        //playerHealthChanged?.Invoke();
    }

    private void Die()
    {
        Destroy(gameObject);
        GameManager.Instance.GameOver();
    }
}
