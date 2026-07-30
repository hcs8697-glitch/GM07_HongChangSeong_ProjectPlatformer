using UnityEngine;

public class PatrolState : IEnemyState
{
    private EnemyController enemy;
    private PlayerChecker playerChecker;

    public PatrolState(EnemyController enemy, PlayerChecker playerChecker)
    {
        this.enemy = enemy;
        this.playerChecker = playerChecker;
    }

    public void Enter()
    {

    }

    public void Stay()
    {
        if(playerChecker.IsDetected)
        {
            enemy.EnemyStateMachine.TransitionTo(enemy.EnemyStateMachine.ChaseState);
            return;
        }
        if(enemy.IsWaiting)
        {
            enemy.EnemyStateMachine.TransitionTo(enemy.EnemyStateMachine.EnemyIdleState);
        }
    }

    public void Exit()
    {

    }
}
