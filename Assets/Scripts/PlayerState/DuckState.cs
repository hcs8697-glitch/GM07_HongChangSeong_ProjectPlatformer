using UnityEngine;

public class DuckState : IState
{
    private Player player;

    public DuckState(Player player)
    {
        this.player = player;
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

        if (player.Controller.Rb.linearVelocity.y < 0)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.FallState);
            return;
        }

        if (InputManager.Movement.x != 0)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.Duck_WalkState);            
        }
    }

    public void Exit()
    {

    }

}
