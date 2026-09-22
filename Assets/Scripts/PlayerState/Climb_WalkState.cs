using UnityEngine;

public class Climb_WalkState : IState
{
    private Player player;


    public Climb_WalkState(Player player)
    {
        this.player = player;
    }


    public void Enter()
    {
        player.SetClimb(true);
    }

    public void Stay()
    {
        if(InputManager.Movement.y !=0)
        {
            player.Controller.ClimbWall(player.Controller.ClimbSpeed);
        }
        

        //벽을 타고있는 상태에선 점프 안 되야 하므로 이거 없애야 함
        //if (InputManager.IsJump)
        //{
        //    player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.JumpState);
        //    return;
        //}

        if(InputManager.Movement.y == 0)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.ClimbState);
            return;
        }

        //전환조건을 더 신경써야 함. 이 상태라면 벽을 바라보지 않은 상태에도 점프해버림
        if (!player.WallChecker.IsWall && InputManager.Movement.y >0)
        {
            player.SetClimb(false);
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.JumpState);
            return;
        }

        if (!player.WallChecker.IsWall && InputManager.Movement.y < 0)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.FallState);
            return;
        }


    }

    public void Exit()
    {
        player.SetClimb(false);
    }
}
