using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public enum PlayerAnimState
    {
        Idle,
        Walk,
        Jump,
        Duck,
        Duck_Walk
    }

    private static readonly int StateHash = Animator.StringToHash("State");
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    [SerializeField] private Animator animator;
    [SerializeField] private PlayerAnimState currentState = PlayerAnimState.Idle;


    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    public void SetState(PlayerAnimState newState)
    {
        if (currentState == newState) return;

        currentState = newState;
        animator.SetInteger(StateHash, (int)newState);
    }

    public void SetTrigger()
    {
        animator.SetTrigger(AttackHash);
    }
}
