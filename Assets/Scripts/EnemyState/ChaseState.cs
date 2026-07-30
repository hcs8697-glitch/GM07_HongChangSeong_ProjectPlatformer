using UnityEngine;

public class ChaseState : IEnemyState
{
    private EnemyController enemy;
    private PlayerChecker playerChecker;

    public ChaseState(EnemyController enemy, PlayerChecker playerChecker)
    {
        this.enemy = enemy;
        this.playerChecker = playerChecker;
    }

    public void Enter()
    {

    }

    public void Stay()
    {
        if(!playerChecker.IsDetected)
        {
            enemy.EnemyStateMachine.TransitionTo(enemy.EnemyStateMachine.ReturnState);
        }
    }

    public void Exit()
    {

    }
}
