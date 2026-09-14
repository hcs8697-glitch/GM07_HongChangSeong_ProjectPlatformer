using UnityEngine;

//플레이어의 기본 공격을 담당할 스크립트.
//공격 판정 방식은 Overlap 계열 메서드를 이용해 구현한다.
//다만, 각 상태마다 공격 판정이 다를 수 있고,
//추후에 이 스크립트에서 실행되지 않고 상태머신에서 실행될 수도 있음을 고려한다.


//TODO
//공격 범위를 이 스크립트, 또는 SO에서 결정할 수 있도록 리펙토링
//Scriptable Object를 사용해 다른 공격들의 데이터를 저장한 후 AttackHandler등의 클래스로 갈아끼우게 하기.



public class PlayerAttack : MonoBehaviour
{
    [Header("공격 관련 필드")]
    [SerializeField] private int attackDamage = 30;
    [SerializeField] private PlayerController controller;
    [SerializeField] private Vector2 attackOffset;
    [SerializeField] private Vector2 attackSize;
    [SerializeField] private LayerMask damageableLayer;
    [SerializeField] private float attackCooldown = 2.0f;

    private float lastAttackTime = 0.0f;

    public bool CanAttack => Time.time >= lastAttackTime + attackCooldown;


    public void Attack()
    {
        lastAttackTime = Time.time;

        Vector2 center = (Vector2)transform.position + new Vector2(attackOffset.x * controller.FacingDirection, attackOffset.y);

        Collider2D[] hits = Physics2D.OverlapBoxAll(center, attackSize, 0.0f, damageableLayer);

        //아무것도 맞지 않았다면 종료
        if (hits.Length == 0) return;

        //무언가 맞았으므로 
        AudioManager.Instance.PlaySFX(ESfx.SFX_Hit);

        foreach (Collider2D hit in hits)
        {
            if(hit.TryGetComponent<IDamageable>(out IDamageable target))
            {
                target.TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (controller == null) return;

        Vector2 center = (Vector2)transform.position + new Vector2(attackOffset.x * controller.FacingDirection, attackOffset.y);

        if(!CanAttack)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawWireCube(center, attackSize);
    }
}
