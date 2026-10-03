using UnityEngine;

//플레이어와 적이 발판으로 삼을 수 있는 함정.
//파괴할 수 없다.
//현 시점에선, 스프라이트 변경이 아닌 컬러 변경으로 활성화 / 비활성화를 감지하게 할 수 있다.

public class GroundTrap : MonoBehaviour
{
    [Header("피해량")]
    [SerializeField] private int damage = 1;
    [Header("트랩 전환 시간")]
    [SerializeField] private float toggleTime = 1.0f;
    private WaitForSeconds wait;
    [Header("최초에 켜져있는지")]
    [SerializeField] private bool isTrapOn;
    [Header("활성화 시 변경될 스프라이트")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color originalColor;
    [SerializeField] private Color changedColor = Color.red;

    private Coroutine activeCoroutine;

    private void Awake()
    {
        wait = new WaitForSeconds(toggleTime);
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer.sprite == null)
        {
            Debug.LogWarning($"[GroundTrap] : 기본 스프라이트가 없습니다. {this.name}");
            return;
        }
        originalColor = spriteRenderer.color;

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(ToggleTrap), toggleTime, toggleTime);
    }


    private void ToggleTrap()
    {
        isTrapOn = !isTrapOn;        

        if (isTrapOn)
        {
            spriteRenderer.color = changedColor;
        }
        else if (!isTrapOn)
        {
            spriteRenderer.color = originalColor;
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isTrapOn) return;

        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            Debug.Log("IDamageable이 있는 무언가가 들어옴.");
            damageable.TakeDamage(damage);
        }
    }


}
