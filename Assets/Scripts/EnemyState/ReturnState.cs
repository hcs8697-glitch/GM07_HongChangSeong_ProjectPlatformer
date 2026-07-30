using UnityEngine;

public class ReturnState : IEnemyState
{
    private EnemyController enemy;
    private PlayerChecker playerChecker;

    public ReturnState(EnemyController enemy, PlayerChecker playerChecker)
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
        if(!playerChecker.IsDetected && !enemy.IsReturning)
        {
            enemy.EnemyStateMachine.TransitionTo(enemy.EnemyStateMachine.PatrolState);
        }
    }

    public void Exit()
    {

    }
}
