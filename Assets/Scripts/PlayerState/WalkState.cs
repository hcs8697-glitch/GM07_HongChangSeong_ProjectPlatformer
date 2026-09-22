using UnityEngine;

public class WalkState : IState
{
    private Player player;


    public WalkState(Player player)
    {
        this.player = player;

    }

    public void Enter()
    {

    }

    public void Stay()
    {

         player.Controller.Move(player.Controller.WalkSpeed);
         player.AnimationController.SetState(PlayerAnimationController.PlayerAnimState.Walk);


        if (InputManager.IsLeftClicked)
        {
            player.TryAttack();
        }

        if (player.GroundChecker.IsGrounded && InputManager.IsJump)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.JumpState);
            return;
        }
        if(InputManager.Movement.x == 0)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.IdleState);
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
