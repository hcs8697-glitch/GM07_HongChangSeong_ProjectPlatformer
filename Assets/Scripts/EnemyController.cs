using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed = 5.0f; 
    [SerializeField] private PlayerChecker playerChecker;

    [Header("패트롤 거리")]
    [SerializeField] private Vector2 moveOffset = new Vector2(3.0f, 0.0f);
    [Header("패트롤 후 Idle")]
    [SerializeField] private float waitTime = 2.0f;
   
    private WaitForSeconds wait;
    private Rigidbody2D rb;
    private Vector2 startPosition;
    private Vector2 targetPosition;
    private Vector2 destination;
    private SimpleEnemyStateMachine enemyStateMachine;
    private bool isMovingToTarget = true;
    private bool isWaiting = false;
    private bool isReturning = false;


    public bool IsMovingToTarget => isMovingToTarget;
    public bool IsWaiting => isWaiting;
    public bool IsReturning => isReturning;

    public SimpleEnemyStateMachine EnemyStateMachine => enemyStateMachine;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerChecker = GetComponentInChildren<PlayerChecker>();
        startPosition = rb.position;
        targetPosition = rb.position + moveOffset; //transform.position은 Vector3라서 rb.position을 쓰는 게 맞을듯.
        enemyStateMachine = new SimpleEnemyStateMachine(this, playerChecker);
        wait = new WaitForSeconds(waitTime);
    }

    void Start()
    {
        enemyStateMachine.Initialize(enemyStateMachine.EnemyIdleState);
    }

    // Update is called once per frame
    void Update()
    {
        enemyStateMachine.Stay();

        Patrol();
        Chase();
    }

    private void Patrol()
    {
        if (playerChecker.IsDetected || isWaiting) //대기중인 상태이거나 플레이어를 발견한 상태에선 실행되지 않음.
            return;

        isReturning = false;

        if (!playerChecker.IsDetected)
        {
            if(isMovingToTarget)
            {
                destination = targetPosition;
            }
            else
            {
                destination = startPosition;
            }

            //TODO : linearVelocity를 이용하는 방향으로 리팩토링
            //현재는 그냥 0605Homework에서 발판 코드 긁어온 걸로 구현됨.
            //이동방식에 문제가 있음. 멈췄을 때 살짝 덜컹거림.

            Vector2 nextPosition = Vector2.MoveTowards(rb.position, destination, moveSpeed * Time.deltaTime);

            rb.MovePosition(nextPosition);

            float distance = Vector2.Distance(rb.position, destination);

            if (distance < 0.01f && !isWaiting) //새로운 코루틴이 매 프레임마다 실행되지 않도록
            {
                StartCoroutine(IdleCo());                
            }
        }
    }

    private void Chase()
    {
        if (player == null) return;

        if(playerChecker.IsDetected)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            if(!playerChecker.IsDetected) //들어왔다가 나간 경우
            {
                Return();
            }
        }
    }

    private IEnumerator IdleCo() //문제는 Patrol이 업데이트에서 실행된다는 것. 새로운 코루틴이 계속 실행되지 않을까?
    {
        isWaiting = true; //true로 바꾸고
        //rb.linearVelocityX = 0f;
        yield return wait; //시간 지난 후
        isMovingToTarget = !isMovingToTarget;
        isWaiting = false; //다시 false로
    }

    private void Return()
    {
        isReturning = true;
        transform.position = Vector2.MoveTowards(transform.position, destination, moveSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 start = transform.position;
        Vector3 end = start + (Vector3)moveOffset;

        Gizmos.DrawLine(start, end);

        Gizmos.DrawWireSphere(start, 0.15f);
        Gizmos.DrawWireSphere(end, 0.15f);
    }
}
