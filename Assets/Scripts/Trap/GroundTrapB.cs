using UnityEngine;

//기존 GroundTrap과 유사하지만, 오로지 스위치에 의해서만 제어될 함정
//규모가 커질 경우 상속을 고려한다.

public class GroundTrapB : Switchable
{
    [Header("피해량")]
    [SerializeField] private int damage = 1;

    [Header("최초에 켜져있는지")]
    [SerializeField] private bool isTrapOn;
    [Header("활성화 시 변경될 스프라이트")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color originalColor;
    [SerializeField] private Color changedColor = Color.red;



    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer.sprite == null)
        {
            Debug.LogWarning($"[GroundTrap] : 기본 스프라이트가 없습니다. {this.name}");
            return;
        }
        originalColor = spriteRenderer.color;
    }

    private void Start()
    {
        RefreshTrapStatus();
    }


    public override void OnSwitchPressed()
    {
        isTrapOn = !isTrapOn;

        RefreshTrapStatus();
    }

    private void RefreshTrapStatus()
    {
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
