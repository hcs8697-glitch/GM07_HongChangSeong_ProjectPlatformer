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
        Debug.Log("DuckState 진입");
        player.SetDuck(true);
    }

    public void Stay()
    {
        player.AnimationController.SetState(PlayerAnimationController.PlayerAnimState.Duck);

        if (InputManager.IsLeftClicked)
        {
            player.TryAttack();
        }

        //S키를 땠을 때 원래대로 돌아가게 한다.
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
        player.SetDuck(false);
    }

}
