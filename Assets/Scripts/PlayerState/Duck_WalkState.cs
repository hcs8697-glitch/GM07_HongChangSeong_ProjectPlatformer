using UnityEngine;


//이걸 필드를 만들어야 할 이유가 있나? Player클래스에서 프로퍼티를 제공하는데.
public class Duck_WalkState : IState
{
    private Player player;


    public Duck_WalkState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.SetDuck(true);
    }

    public void Stay()
    {
        player.Controller.Move(player.Controller.DuckWalkSpeed);
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

        if (player.Controller.Rb.linearVelocity.y < 0)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.FallState);
            return;
        }

        if (InputManager.Movement.y >= 0)
        {
            player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.IdleState);
        }
    }

    public void Exit()
    {
        player.SetDuck(false);
    }
}
