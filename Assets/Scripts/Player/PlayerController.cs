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
    [SerializeField] private float climbSpeed = 5.0f;

    //PlayerController가 이제는 PlayerAttack이랑, AnimationController를 알 필요가 없다. 다 지워야 할 것?
    //다만, 현 시점에서 "점프"의 경우는 알아야 하는 거 같은데... 이것도 그냥 점프 로직만 여기서 제공하고 제약을 상태머신에서 관리하는 게 맞지 않나
    [SerializeField] private GroundChecker groundChecker;


    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private float originalGravity;



    private bool isDucking = false;

    public bool IsDucking => isDucking; 


    public float WalkSpeed => walkSpeed;
    public float DuckWalkSpeed => duckWalkSpeed;
    public float AirWalkSpeed => airWalkSpeed;

    public float ClimbSpeed => climbSpeed;
    public Rigidbody2D Rb => rb;
    public float OriginalGravity => originalGravity;



    //PlayerAttack 등에게 바라보고 있는 방향을 전달할 프로퍼티
    public int FacingDirection { get; private set; } = 1;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(rb != null)
        {
            originalGravity = rb.gravityScale;
        }
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
    }

    public void Move(float moveSpeed)
    {
        float moveX = InputManager.Movement.x; //입력 값에서 x축만 가져옴.

        rb.linearVelocityX = moveX * moveSpeed;
    }

    public void ClimbWall(float climbSpeed)
    {
        float moveY = InputManager.Movement.y;

        rb.linearVelocityY = moveY * climbSpeed;
    }

    //이거를 컨트롤러가 제공하는 게 맞나? 아니면 퍼사드에서 조립할까?
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
}
