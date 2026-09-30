using UnityEngine;

public class Enemy : MonoBehaviour
{
    //player.Attack 이런 식으로 쓸 수 있도록...?
    //private EnemyAnimationController animationController;
    //private PlayerAttack attack;
    private EnemyHealth health;
    private EnemyController controller;
    private PlayerChecker playerChecker;
    //private GroundChecker groundChecker;
    //private WallChecker wallChecker;
    //private CeilChecker ceilChecker;
    private SimpleEnemyStateMachine playerStateMachine;


    //프로퍼티
    //public EnemyAnimationController AnimationController => animationController;
    //public PlayerAttack Attack => attack;
    public EnemyHealth Health => health;
    public EnemyController Controller => controller;
    public PlayerChecker PlayerChecker => playerChecker;
    //public GroundChecker GroundChecker => groundChecker;
    //public WallChecker WallChecker => wallChecker;
    //public CeilChecker CeilChecker => ceilChecker;
    public SimpleEnemyStateMachine PlayerStateMachine => playerStateMachine;


    private void Awake()
    {
        EnsureComponent();
    }




    private void EnsureComponent()
    {
        health = GetComponent<EnemyHealth>();
        controller = GetComponent<EnemyController>();
        playerChecker = GetComponentInChildren<PlayerChecker>();
    }
}
