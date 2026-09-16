using UnityEngine;

public class FallState : IState
{
    private Player player;

    public FallState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {

    }

    public void Stay()
    {
        player.Controller.Move(player.Controller.AirWalkSpeed);

        if (InputManager.IsLeftClicked)
        {
            player.TryAttack();
        }


        if (player.GroundChecker.IsGrounded)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.IdleState);
        }
    }

    public void Exit()
    {

    }
}
