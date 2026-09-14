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
        Debug.Log("JumpState");
        player.Controller.Jump();
        player.AnimationController.SetState(PlayerAnimationController.PlayerAnimState.Jump);
    }

    public void Stay()
    {
        player.Controller.Move(player.Controller.AirWalkSpeed);

        if (InputManager.IsLeftClicked)
        {
            player.TryAttack();
        }

        if (!player.GroundChecker.IsGrounded) return;

        if (player.Controller.Rb.linearVelocity.y > 0) return;

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
