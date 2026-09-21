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

        //벽을 감지하지 못했을 때 전환도 해야 함.


        if(InputManager.IsJump)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.JumpState);
            return;
        }

        if(InputManager.Movement.y != 0)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.Climb_WalkState);
            return;
        }

        //여기에서 만약에 현재 바라보고 있는 키를 오래 누르면, FallState로 전환


    }

    public void Exit()
    {
        player.SetClimb(false);
    }
}
