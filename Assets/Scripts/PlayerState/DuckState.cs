using UnityEngine;

public class DuckState : IState
{
    private PlayerController player;
    private GroundChecker playerGroundChecker;

    public DuckState(PlayerController player, GroundChecker playerGroundChecker)
    {
        this.player = player;
        this.playerGroundChecker = playerGroundChecker;
    }

    public void Enter()
    {
        Debug.Log("DuckState");
    }

    public void Stay()
    {
        player.AnimationController.SetState(PlayerAnimationController.PlayerAnimState.Duck);

        if (InputManager.IsLeftClicked)
        {
            player.TryAttack();
        }

        if (InputManager.Movement.y >=0)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.IdleState);
            return;
        }
        if(InputManager.Movement.x != 0)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.Duck_WalkState);            
        }
    }

    public void Exit()
    {

    }

}
