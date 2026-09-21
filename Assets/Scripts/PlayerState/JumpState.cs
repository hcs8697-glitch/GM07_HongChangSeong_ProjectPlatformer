using UnityEngine;

public class JumpState : IState
{
    private Player player;

    public JumpState(Player player)
    {
        this.player = player;

    }

    public void Enter()
    {
        Debug.Log("JumpState 진입");
        player.Controller.Jump();
        player.AnimationController.SetState(PlayerAnimationController.PlayerAnimState.Jump);
    }

    public void Stay()
    {
        //점프 중에는 공중 이동속도로 이동할 수 있다.
        player.Controller.Move(player.Controller.AirWalkSpeed);

        //점프 중에는 공격을 시도할 수 있다.
        if (InputManager.IsLeftClicked)
        {
            player.TryAttack();
        }

        //만약 linearVelocity.y가 0보다 작다 = 즉 어디선가에서 떨어지고 있다.
        if (player.Controller.Rb.linearVelocity.y < 0)
        {
            //상태를 떨어지는 상태로 바꾸고 이하의 코드는 실행하지 않는다.
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.FallState);
            return;
        }

        //플레이어가 땅이 아닌 상태라면 점프 도중에 실행해야 하는 것들을 실행하지 않는다 
        if (!player.GroundChecker.IsGrounded) return;

        //플레이어가 상승중이라면 이하의 코드들을 실행하지 않는다. 
        if (player.Controller.Rb.linearVelocity.y > 0) return;


        //위를 전부 통과했다 = linearVelocity가 0 이하이고, 땅을 감지했다.
        if (InputManager.Movement.x != 0)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.WalkState);
        }
        else
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.IdleState);
        }
    }

    public void Exit()
    {

    }
}
