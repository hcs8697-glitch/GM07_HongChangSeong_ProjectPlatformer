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
        //enum을 아예 통째로 관리하던가 해야 할 듯.
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
        if (InputManager.Movement.y < 0 && player.GroundChecker.IsGrounded)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.DuckState);
        }
    }

    public void Exit()
    {

    }
}
