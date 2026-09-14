using UnityEngine;
using System;

[Serializable]
public class SimplePlayerStateMachine
{
    public IState CurrentState { get; private set; }

    public IdleState IdleState { get; private set; }
    public WalkState WalkState { get; private set; }
    public JumpState JumpState { get; private set; }
    public DuckState DuckState { get; private set; }

    public Duck_WalkState Duck_WalkState { get; private set; }

    public event Action<IState> stateChanged;

    public SimplePlayerStateMachine(Player player)
    {
        IdleState = new IdleState(player);
        WalkState = new WalkState(player);
        JumpState = new JumpState(player);
        DuckState = new DuckState(player);
        Duck_WalkState = new Duck_WalkState(player);
    }

    public void Initialize(IState state)
    {
        CurrentState = state;

        CurrentState.Enter();

        stateChanged?.Invoke(CurrentState);
    }

    public void TransitionTo (IState nextState)
    {
        if (CurrentState == nextState) return;

        CurrentState.Exit();

        CurrentState = nextState;

        CurrentState.Enter();

        stateChanged?.Invoke(CurrentState);
    }

    public void Stay()
    {
        if (CurrentState == null) return;

        CurrentState.Stay();
    }
}
