using UnityEngine;

public class JumpState : IState
{
    private PlayerController player;
    private GroundChecker playerGroundChecker;

    public JumpState(PlayerController player, GroundChecker playerGroundChecker)
    {
        this.player = player;
        this.playerGroundChecker = playerGroundChecker;
    }

    public void Enter()
    {
        Debug.Log("JumpState");
        player.Jump();
        player.AnimationController.SetState(PlayerAnimationController.PlayerAnimState.Jump);
    }

    public void Stay()
    {
        player.Move(player.AirWalkSpeed);

        if (InputManager.IsLeftClicked)
        {
            player.TryAttack();
        }

        if (!playerGroundChecker.IsGrounded) return;

        if (player.Rb.linearVelocity.y > 0) return;

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
