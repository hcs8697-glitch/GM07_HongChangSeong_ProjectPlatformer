using UnityEngine;

public class FallZone : MonoBehaviour
{
    [Header("바꿀 일 없는 대미지")]
    [SerializeField] private int damage = 9999;
    [Header("영역 크기")]
    [SerializeField] private Vector2 size = new Vector2(3, 3);

    private BoxCollider2D boxCollider;


    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.size = size;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable))
        {
            Debug.Log("IDamageable이 있는 무언가가 들어옴.");
            damageable.TakeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireCube(transform.position, size);
    }
}
