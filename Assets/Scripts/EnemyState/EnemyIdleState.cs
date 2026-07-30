using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private EnemyController enemy;
    private PlayerChecker playerChecker;

    public EnemyIdleState(EnemyController enemy, PlayerChecker playerChecker)
    {
        this.enemy = enemy;
        this.playerChecker = playerChecker;
    }

    public void Enter()
    {

    }

    public void Stay()
    {
        if(!enemy.IsWaiting)
        {
            enemy.EnemyStateMachine.TransitionTo(enemy.EnemyStateMachine.PatrolState);
            return;
        }

        if(playerChecker.IsDetected)
        {
            enemy.EnemyStateMachine.TransitionTo(enemy.EnemyStateMachine.ChaseState);
        }
    }

    public void Exit()
    {

    }
}
