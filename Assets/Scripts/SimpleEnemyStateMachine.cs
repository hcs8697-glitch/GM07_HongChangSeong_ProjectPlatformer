using System;
using UnityEngine;

[Serializable]
public class SimpleEnemyStateMachine
{
    public IEnemyState CurrentState { get; private set; }

    public EnemyIdleState EnemyIdleState { get; private set; }
    public PatrolState PatrolState { get; private set; }
    public ChaseState ChaseState { get; private set; }
    public ReturnState ReturnState { get; private set; }

    public event Action<IEnemyState> enemyStateChanged;

    public SimpleEnemyStateMachine(EnemyController enemy, PlayerChecker playerChecker)
    {
        EnemyIdleState = new EnemyIdleState(enemy, playerChecker);
        PatrolState = new PatrolState(enemy, playerChecker);
        ChaseState = new ChaseState(enemy, playerChecker);
        ReturnState = new ReturnState(enemy, playerChecker);
    }

    public void Initialize(IEnemyState state)
    {
        CurrentState = state;

        CurrentState.Enter();

        enemyStateChanged?.Invoke(CurrentState);
    }

    public void TransitionTo(IEnemyState nextState)
    {
        if (CurrentState == nextState) return;

        CurrentState.Exit();

        CurrentState = nextState;

        CurrentState.Enter();

        enemyStateChanged?.Invoke(CurrentState);
    }

    public void Stay()
    {
        if (CurrentState == null) return;

        CurrentState.Stay();
    }
}
