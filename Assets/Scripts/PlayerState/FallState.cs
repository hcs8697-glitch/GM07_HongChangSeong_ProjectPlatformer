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
        Debug.Log("FallState 진입");

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
            return;
        }


        //공중에 떠있고, 동시에 벽을 감지했다면 + 인풋이 있다면? 문제는 인풋이... 내가 바라보고 있는 방향으로 있는지 확인해야하는데?
        //공중에 떠있고, 벽을 감지했으며, 동시에 플레이어의 인풋이 현재 바라보고 있는 방향과 동일하다???
        //아 근데 이거 애매하네. FacingDirection은 1또는 -1인데, Movement.x 는 수치가 몇이지?
        if (!player.GroundChecker.IsGrounded && player.WallChecker.IsWall && InputManager.Movement.x == player.Controller.FacingDirection)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.ClimbState);
        }


    }

    public void Exit()
    {

    }
}
