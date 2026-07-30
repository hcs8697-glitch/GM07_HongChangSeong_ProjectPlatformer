using UnityEngine;

public class Duck_WalkState : IState
{
    private PlayerController player;
    private GroundChecker playerGroundChecker;

    public Duck_WalkState(PlayerController player, GroundChecker playerGroundChecker)
    {
        this.player = player;
        this.playerGroundChecker = playerGroundChecker;
    }

    public void Enter()
    {

    }

    public void Stay()
    {
        player.Move(player.DuckWalkSpeed);
        player.AnimationController.SetState(PlayerAnimationController.PlayerAnimState.Duck_Walk);

        if (InputManager.IsLeftClicked)
        {
            player.TryAttack();
        }

        if (InputManager.Movement.x == 0)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.DuckState);
            return;
        }
        if(InputManager.Movement.y >= 0)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.IdleState);
        }
    }

    public void Exit()
    {

    }
}
