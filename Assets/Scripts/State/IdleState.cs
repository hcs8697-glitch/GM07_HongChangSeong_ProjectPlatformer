using UnityEngine;

public class IdleState : IState
{
    private PlayerController player;
    private GroundChecker playerGroundChecker;

    public IdleState(PlayerController player, GroundChecker playerGroundChecker)
    {
        this.player = player;
        this.playerGroundChecker = playerGroundChecker;
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

        if (playerGroundChecker.IsGrounded && InputManager.IsJump)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.JumpState);
            return;
        }
        if(InputManager.Movement.x != 0)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.WalkState);
            return;
        }
        if(InputManager.Movement.y < 0 && playerGroundChecker.IsGrounded)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.DuckState);
        }
    }

    public void Exit()
    {

    }

}
