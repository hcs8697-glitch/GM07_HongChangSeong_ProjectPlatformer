using UnityEngine;

public class ClimbState : IState
{
    private Player player;

    public ClimbState(Player player)
    {
        this.player = player;
    }



    public void Enter()
    {
        player.SetClimb(true);
    }

    public void Stay()
    {
        //매달리고 있는 도중 땅을 감지했다면 자동으로 내려가게
        if(player.GroundChecker.IsGrounded)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.IdleState);
            return;
        }

        if(InputManager.IsJump)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.JumpState);
        }
    }

    public void Exit()
    {
        player.SetClimb(false);
    }
}
