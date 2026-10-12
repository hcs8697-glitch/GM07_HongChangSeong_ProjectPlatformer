using UnityEngine;

public class DestroyableObject : MonoBehaviour, IDamageable
{
    [Header("최대 체력")]
    [SerializeField] private int maxHp = 3;
    //현재 체력
    [SerializeField] private int currentHp;
    public Team Team => Team.Obstacle;
    [Header("소환할 아이템")]
    [SerializeField] private ItemData itemData;

    public int CurrentHp
    {
        get => currentHp;

        private set
        {
            currentHp = Mathf.Clamp(value, 0, maxHp);


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
    }

    private void Die()
    {
        if(itemData != null)
        {
            GameManager.Instance.Reward.SpawnItemBySO(itemData, transform.position);
        }

        Destroy(gameObject);
    }

}
