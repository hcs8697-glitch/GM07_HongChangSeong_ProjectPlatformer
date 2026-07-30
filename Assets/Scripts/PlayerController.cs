using UnityEngine;
//Rigidbody2D 컴포넌트를 이용해 움직임을 구현할 것.
//각 상태의 로직(이동, 점프, 공격)은 이 스크립트에 넣는다. 수정해야 할 경우 이 스크립트만 수정하게 한다.
//각 이동 메서드들은 이 스크립트의 Update가 아니라, 각 State에서 실행되어야만 한다.


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("이동속도")]
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float duckWalkSpeed = 3.3f;
    [SerializeField] private float airWalkSpeed = 5.0f;
    [SerializeField] private float jumpPower = 6.0f;

    [SerializeField] private GroundChecker groundChecker; //인스펙터에서 넣으니 GetComponentInChildren은 필요 없을 수 있음.

    [SerializeField] private PlayerAttack playerAttack;

    [SerializeField] private PlayerAnimationController animationController;

    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;

    private SimplePlayerStateMachine playerStateMachine;

    private bool isDucking = false;

    public bool IsDucking => isDucking; 


    public float WalkSpeed => walkSpeed;
    public float DuckWalkSpeed => duckWalkSpeed;
    public float AirWalkSpeed => airWalkSpeed;
    public Rigidbody2D Rb => rb;

    public PlayerAttack PlayerAttack => playerAttack;

    public PlayerAnimationController AnimationController => animationController;

    public SimplePlayerStateMachine PlayerStateMachine => playerStateMachine;

    //PlayerAttack 등에게 바라보고 있는 방향을 전달할 프로퍼티
    public int FacingDirection { get; private set; } = 1;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundChecker = GetComponentInChildren<GroundChecker>();
        playerStateMachine = new SimplePlayerStateMachine(this, groundChecker);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        playerStateMachine.Initialize(playerStateMachine.IdleState);
    }

    void Update()
    {
        if(InputManager.Movement.x>0)
        {
            spriteRenderer.flipX = true;
            FacingDirection = 1;
        }
        else if (InputManager.Movement.x<0)
        {
            spriteRenderer.flipX = false;
            FacingDirection = -1;
        }

        playerStateMachine.Stay();
    }

    public void Move(float moveSpeed)
    {
        float moveX = InputManager.Movement.x; //입력 값에서 x축만 가져옴.

        rb.linearVelocityX = moveX * moveSpeed;
    }

    private void Duck()
    {
        if (InputManager.Movement.y <0 && groundChecker.IsGrounded) //S키를 누르고 있다면 숙이기
        {
            Debug.Log("숙임 적용");
            isDucking = true;
        }
        else
        {
            Debug.Log("숙임 해제");
            isDucking = false;
        }
    }


    public void Jump()
    {
            AudioManager.Instance.PlaySFX(ESfx.SFX_Jump);
            isDucking = false;
            rb.linearVelocityY = jumpPower;
    }

    public void TryAttack()
    {     
        if (!playerAttack.CanAttack) return;

        playerAttack.Attack();
        animationController.SetTrigger();
        AudioManager.Instance.PlaySFX(ESfx.SFX_Attack);
    }
}
