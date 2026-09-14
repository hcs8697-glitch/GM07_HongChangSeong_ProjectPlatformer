using UnityEngine;

public class WalkState : IState
{
    private PlayerController player;
    private GroundChecker playerGroundChecker;

    public WalkState(PlayerController player, GroundChecker playerGroundChecker)
    {
        this.player = player;
        this.playerGroundChecker = playerGroundChecker;
    }

    public void Enter()
    {

    }

    public void Stay()
    {
        player.Move(player.WalkSpeed);
        //enum을 아예 통째로 관리하던가 해야 할 듯.
        player.AnimationController.SetState(PlayerAnimationController.PlayerAnimState.Walk);

        if (InputManager.IsLeftClicked)
        {
            player.TryAttack();
        }

        if (playerGroundChecker.IsGrounded && InputManager.IsJump)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.JumpState);
            return;
        }
        if(InputManager.Movement.x == 0)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.IdleState);
            return;
        }
        if (InputManager.Movement.y < 0 && playerGroundChecker.IsGrounded)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.DuckState);
        }
    }

    public void Exit()
    {

    }
}
