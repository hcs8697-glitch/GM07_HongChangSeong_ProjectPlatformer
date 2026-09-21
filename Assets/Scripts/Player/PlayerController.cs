using UnityEngine;
//Rigidbody2D 컴포넌트를 이용해 움직임을 구현할 것.
//각 상태의 로직(이동, 점프, 공격)은 이 스크립트에 넣는다. 수정해야 할 경우 이 스크립트만 수정하게 한다.
//각 이동 메서드들은 이 스크립트의 Update가 아니라, 각 State에서 실행되어야만 한다.


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
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
    [SerializeField] private WallChecker wallChecker;

    [Header("피격판정범위")]
    [SerializeField] private Vector2 originalColSize;
    [SerializeField] private Vector2 originalColOffset;
    [SerializeField] private float duckColY = 1f;

    private SpriteRenderer spriteRenderer;
    private Player player;
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private float originalGravity;



    private bool isDucking = false;

    public bool IsDucking => isDucking;


    public float WalkSpeed => walkSpeed;
    public float DuckWalkSpeed => duckWalkSpeed;
    public float AirWalkSpeed => airWalkSpeed;

    public float ClimbSpeed => climbSpeed;
    public Rigidbody2D Rb => rb;
    public float OriginalGravity => originalGravity;

    public BoxCollider2D Col => col;



    //PlayerAttack 등에게 바라보고 있는 방향을 전달할 프로퍼티
    [field : SerializeField]public int FacingDirection { get; private set; } = 1;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponent<Player>();
        col = GetComponent<BoxCollider2D>();

        //원본 중력값 저장
        if (rb != null)
        {
            originalGravity = rb.gravityScale;
        }

        //원본 콜라이더 크기 저장
        if (col != null)
        {
            originalColSize = col.size;
            originalColOffset = col.offset;
        }
    }


    void Update()
    {

        //가급적이는 퍼사드로서 기능하는 Player클래스를 하위 클래스들이 알게 하지 않는 구조로 만들고 싶은데,
        //문제는 상태에 따라서 플립하지 않아야 하는 경우가 있단 말임?
        //이하의 기능을 PlayerFlip같은 하위 클래스로 분리한다고 하더라도
        //해당 클래스가 그걸 알긴 해야 해. 그러니 분리하는 건 사실상 큰 의미가 없어. 여기 코드가 깔끔해진다 정도긴 한데.
        //이걸 Player클래스에 옮겨도 문제인게
        //얘 하나만 알아도 될 걸 월체커랑 그라운드체커 둘 다 Player를 알아야 함.
        //그냥 각자가 다 이하의 코드를 가지면 의존도는 줄어드는데, 중복코드가 계속 발생하고.

        //플레이이어의 현재 상태를 확인하고, 만일 ClimbState나 Climb_WalkState라면 플립되지 않게 한다.
        if (player.PlayerStateMachine.CurrentState == player.PlayerStateMachine.Climb_WalkState || player.PlayerStateMachine.CurrentState == player.PlayerStateMachine.ClimbState)
        {
            return;
        }

        if(wallChecker.IsWall) //벽을 감지했으면 플립하지 않는다? 이거 맞나?
        {
            return;
        }

        if (InputManager.Movement.x > 0)
        {
            spriteRenderer.flipX = true;
            FacingDirection = 1;
        }
        else if (InputManager.Movement.x < 0)
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
        if (InputManager.Movement.y < 0 && groundChecker.IsGrounded) //S키를 누르고 있다면 숙이기
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

    public void Duck(bool toggle)
    {
        switch(toggle)
        {
            case true:
                col.size = new Vector2(originalColSize.x, duckColY);
                float offsetY = (duckColY - originalColSize.y) * 0.5f;
                col.offset = new Vector2(originalColOffset.x, originalColOffset.y + offsetY);
                isDucking = true;                
                break;
             case false:
                col.size = originalColSize;
                col.offset = originalColOffset;
                isDucking = false;
                break;
        }
    }


    public void Jump()
    {
        Debug.Log("일반점프");
        isDucking = false;
         rb.linearVelocityY = jumpPower;
    }

    public void WallJump()
    {
        Debug.Log("벽점프");
        isDucking = false;
        rb.linearVelocity = new Vector2(-FacingDirection*jumpPower, jumpPower);
    }



    private void OnDrawGizmos()
    {
        
    }
}
