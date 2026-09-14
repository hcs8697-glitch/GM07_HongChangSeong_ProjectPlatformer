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

    public SimplePlayerStateMachine(PlayerController player, GroundChecker playerGroundChecker)
    {
        IdleState = new IdleState(player, playerGroundChecker);
        WalkState = new WalkState(player, playerGroundChecker);
        JumpState = new JumpState(player, playerGroundChecker);
        DuckState = new DuckState(player, playerGroundChecker);
        Duck_WalkState = new Duck_WalkState(player, playerGroundChecker);
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
