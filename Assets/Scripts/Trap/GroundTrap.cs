using System.Collections;
using UnityEngine;
using UnityEngine.UI;


//시간에따라 활성화/ 비활성화되어 플레이어와 적 모두에게 피해를 입히는, 부술 수 있는 함정
[RequireComponent(typeof(SpriteRenderer))]
public class GroundTrap : MonoBehaviour, IDamageable
{


    [Header("최대 체력")]
    [SerializeField] private int maxHp = 3;
    //현재 체력
    [SerializeField] private int currentHp;
    [Header("피해량")]
    [SerializeField] private int damage = 1;
    [Header("트랩 전환 시간")]
    [SerializeField] private float toggleTime = 1.0f;
    private WaitForSeconds wait;
    [Header("최초에 켜져있는지")]
    [SerializeField] private bool isTrapOn;
    [Header("활성화 시 변경될 스프라이트")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite originalSprite;
    [SerializeField] private Sprite changedSprite;
   


    public Team Team => Team.Obstacle;

    private Coroutine activeCoroutine;


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
        wait = new WaitForSeconds(toggleTime);
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(spriteRenderer.sprite == null)
        {
            Debug.LogWarning($"[GroundTrap] : 기본 스프라이트가 없습니다. {this.name}");
            return;
        }
        originalSprite = spriteRenderer.sprite;

    }



    //TODO : 플레이어가 근처에 왔을 때만 활성화 / 비활성화가 실행되게
    //아마도 청크를 구현하면 거기서 처리할 듯
    //InvokeRepeating으로 해도 되는 것 아닌가?

    //켜지기 전 경고 표시, 켜지고 꺼지는 시간이 다르게 하기, 특정 조건에서 다르게 동작하기 등을 구현하고자 한다면
    //코루틴으로 전환해야 한다.
    void Start()
    {
        //activeCoroutine = StartCoroutine(ToggleTrapCo());
        InvokeRepeating(nameof(ToggleTrap), toggleTime, toggleTime);
    }

    public void TakeDamage(int damage)
    {
        CurrentHp -= damage;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //꺼져있으면 피해 안 줌
        if (!isTrapOn) return;               
        
        if (collision.TryGetComponent(out IDamageable damageable))
        {
            Debug.Log("IDamageable이 있는 무언가가 들어옴.");
            damageable.TakeDamage(damage);
        }
    }

    private IEnumerator ToggleTrapCo()
    {
        yield return wait;
        ToggleTrap();
    }

    //활성화 / 비활성화에 따라 스프라이트를 바꿀 메서드
    //애니메이션을 적용한다면, Sprite 방식 버리고 SetTrigger를 사용하면 될 듯?
    private void ToggleTrap()
    {
        isTrapOn = !isTrapOn;

        Debug.Log($"[GroundTrap] : 토글함. 현재 트랩의 상태 {isTrapOn}");

        if (isTrapOn)
        {
            spriteRenderer.sprite = changedSprite;
        }
        else if (!isTrapOn)
        {
            spriteRenderer.sprite = originalSprite;
        }
    }


    private void Die()
    {
        if(activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
        }

        CancelInvoke(nameof(ToggleTrap));

        Destroy(gameObject);
    }    
}
