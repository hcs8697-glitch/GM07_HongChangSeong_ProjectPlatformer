using UnityEngine;

//점프를 할 수 있는지 없는지를 판별하게 할 스크립트
//만약 적도 점프를 할 수 있다면, 플레이어를 직접 참조하지 않는 형태이기 때문에 사용 가능하다.
public class GroundChecker : MonoBehaviour
{
    [Header("그라운드 체크 영역 조절")]
    [SerializeField] private float checkerSize1 = 0.3f; //바닥체크용 박스 크기
    [SerializeField] private float checkerSize2 = 0.3f; 
    [SerializeField] private float checkerSize3 = 1.0f; //아마도 이건 건들지 않을 듯.
    [Header("탐색할 레이어")]
    [SerializeField] private LayerMask groundLayers;

    public bool IsGrounded { get; private set; } //이 스크립트를 적도 사용하려면 static이 아니어야 한다.

    private Vector3 checkerSize; //위의 checkerSize1~3까지는 대체 왜 있는 거지? 그냥 이거 SerializeField 하면 되는데?

    private void Awake()
    {
        checkerSize = new Vector3(checkerSize1, checkerSize2, checkerSize3);
    }

    void Update()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        Collider2D hit = Physics2D.OverlapBox(transform.position, checkerSize, 0f, groundLayers);

        if (hit == true)
        {
            IsGrounded = true;
        }
        else if (hit == false)
        {
            IsGrounded = false;
        } 
    }

    private void OnDrawGizmos()
    {
        if (IsGrounded == false) //땅에 없는 상태
        {
            Gizmos.color = Color.red;
        }
        else if (IsGrounded == true)
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawWireCube(transform.position, new Vector3(checkerSize1, checkerSize2, checkerSize3));        
    }
}
