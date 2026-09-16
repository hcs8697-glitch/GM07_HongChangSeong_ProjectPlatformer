using UnityEngine;

public class IdleState : IState
{
    private Player player;


    public IdleState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {

    }

    public void Stay()
    {
        player.AnimationController.SetState(PlayerAnimationController.PlayerAnimState.Idle);

        if (InputManager.IsLeftClicked)
        {
            player.TryAttack();
        }

        if (player.GroundChecker.IsGrounded && InputManager.IsJump)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.JumpState);
            return;
        }
        if(InputManager.Movement.x != 0)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.WalkState);
            return;
        }

        if (player.Controller.Rb.linearVelocity.y < 0)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.FallState);
            return;
        }


        if (InputManager.Movement.y < 0 && player.GroundChecker.IsGrounded)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.DuckState);
        }
    }

    public void Exit()
    {

    }

}
