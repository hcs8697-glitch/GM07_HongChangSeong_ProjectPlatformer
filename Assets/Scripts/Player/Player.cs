using Unity.VisualScripting;
using UnityEngine;

//플레이어의 상태를 관장하는 최상위클래스
//상태머신을 만들 때, 이 클래스만 생성자로 전달하고, 나머지는 player. 이런 식으로 호출하는 구조를 만든다.

//그니까 싱글톤은 아마도 아닐 거잖아.
//정적 인스턴스하나 만들면 접근은 편한데 Player.Instance.EWTWETwet
//안 만들면, 필드로 가져야하고.

//player내부 클래스들은 서로 필드를 갖게 해야 할 것인지? 플레이어 외부의 클래스들,
//가령 적이나 UI와 같은 경우에는 이벤트 기반으로 해야  할 것인지
//이벤트 기반으로 한다고 할지라도, 결국에는 정적 인스턴스 안 만들면 필드로 가져야 하거든?


//퍼사드 패턴을 고려한다. 각 클래스에서 메서드를 제공하고
//이 퍼사드에서 조립하여 그걸 상태머신이 활용할 수 있는 구조를 만든다.

public class Player : MonoBehaviour
{
    //player.Attack 이런 식으로 쓸 수 있도록...?
    private PlayerAnimationController animationController;
    private PlayerAttack attack;
    private PlayerHealth health;
    private PlayerController controller;
    private GroundChecker groundChecker;
    private WallChecker wallChecker;
    private SimplePlayerStateMachine playerStateMachine;
    

    //프로퍼티
    public PlayerAnimationController AnimationController => animationController;
    public PlayerAttack Attack => attack;
    public PlayerHealth Health => health;
    public PlayerController Controller => controller;
    public GroundChecker GroundChecker => groundChecker;
    public WallChecker WallChecker => wallChecker;
    public SimplePlayerStateMachine PlayerStateMachine => playerStateMachine;
    



    private void Awake()
    {
        EnsureComponents();
        //Awake에서 생성자를 통해 상태머신을 생성한다.
        playerStateMachine = new SimplePlayerStateMachine(this);
    }

    //상태머신을 갖는 클래스가 초기화를 하고
    private void Start()
    {
        playerStateMachine.Initialize(playerStateMachine.IdleState);
    }

    //상태머신을 갖는 클래스가 Stay만 업데이트에서 호출하면 알아서 되는 구조
    private void Update()
    {
        playerStateMachine.Stay();
    }



    //퍼사드 메서드
    //고민을 해야하는게, 여기에서 메서드를 조립해서 상태머신에 전달할지
    //아니면은 상태머신이 알아서 필요한 거 하나씩 호출할지 생각해야한다.
    //근데 조립이 맞는 것 같긴 해.
    public void TryAttack()
    {
        if (!attack.CanAttack) return;

        attack.Attack();
        animationController.SetTrigger();
        AudioManager.Instance.PlaySFX(ESfx.SFX_Attack);
    }


    //true를 넣으면 컬라이더의 y축값을 반토막내고, false를 넣으면 원래대로 돌아오게 할 메서드
    //Duck 관련 상태의 Enter에서 true를 넣고, Exit에서 false를 넣는 식으로 하면 될 것 같긴 함.
    //TODO : Collider를 여기서 필드로 갖게.
    //이것도 이하의 SetClimb와 마찬가지로 로직은 다른 곳에서 들고 있고, 여기서 사운드만 조립하는 식으로 변경해야 함.
    //딸랑 이거 한 줄만 적어도 되는 건데, 그럴 거면 PlayerController에 있는 메서드를 호출하는 게 맞지 않나?
    //근데 여기에 컬라이더 수치나 이런 거 넣는거도 애매하지.
    //얘는 그냥 다른 클래스들만 들고 있어야지 수치 조절을 제어하는 클래스가 되선 안 됨.
    public void SetDuck(bool toggle)
    {
        controller.Duck(toggle);
    }

    //Climb 상태와 관련된 곳의 Enter, Exit에서 실행하여, 벽에 달라붙는 상태를 제어할 메서드
    //원래라면 이 메서드는 PlayerController가 들고 있고, 여기서는 사운드만 이 메서드랑 조립해서 TryClimb이런식으로 조립해야 할 것
    //이 두 메서드는 사실 Enter, Exit에서만 호출되기 때문에 애니메이션까지 조립해선 안 됨.

    public void SetClimb(bool toggle)
    {
        switch (toggle)
        {
            case true:
                controller.Rb.gravityScale = 0;
                controller.Rb.linearVelocity = new Vector2(0,0);
                break;
            case false:
                controller.Rb.gravityScale = controller.OriginalGravity;
                break;
        }
    }

    //이전 상태에 따라 다른 점프로직을 실행할 메서드
    //현 시점에선 사실, 벽에 붙어있는지 아닌지만 상관있긴 한데
    public void TryJump(IState previousState)
    {
        AudioManager.Instance.PlaySFX(ESfx.SFX_Jump);
        if (previousState == playerStateMachine.ClimbState)
        {
            controller.WallJump();
        }
        else
        {
            controller.Jump();
        }
    }

    //TryMove메서드 만들 거면, Move와 ClimbWall을 조립하고, Istate와 float(속도)를 매개변수로 받고 this를 넣으면 될 것 같다.
    //그리고, 올라갈 때와 내려갈 때 애니메이션 다르게 하는 건 새로운 상태 파지 말고
    //InputManager.movement.y값을 검사해서 다른 애니메이션 실행하게 하면 될 듯?



    //각 상태에 따라 다른 공격을 실행하게 할 메서드
    public void TryAttack(IState state)
    {
        //스위치문으로 하는 게 안 될 것 같다. 일단 들어온 state를 다운캐스팅해야 하지 않을까?

        if (state == null) return;

        //이런 식으로 해야 하나?
        if(state is FallState)
        {

        }

        //아니면 이런 식으로도 할 수 있겠지

        if(controller.IsDucking)
        {
            //숙였을 때의 공격
        }

        if(!groundChecker.IsGrounded)
        {
            //공중 공격
        }

        


    }


    //초기 실행될 때, 필요한 컴포넌트를 추가하고, 추가하지 못했다면 오류를 제공할 메서드
    //TryGetComponent를 사용하여, 오류 문구를 출력하게 하거나, 이 메서드가 반환값이 bool이어서, false면 뭔가 실행 안 되게끔

    private void EnsureComponents()
    {      

        animationController = GetComponent<PlayerAnimationController>();
        controller = GetComponent<PlayerController>();
        health = GetComponent<PlayerHealth>();
        attack = GetComponent<PlayerAttack>();
        groundChecker = GetComponentInChildren<GroundChecker>();
        wallChecker = GetComponentInChildren<WallChecker>();


    }


}
