using UnityEngine;

//숙인 상태에서 천장을 감지했을 때, 일어서지 못하게 할 클래스
public class CeilChecker : MonoBehaviour
{
    [Header("천장 체크 영역 조절")]
    [SerializeField] private float checkerSize1 = 0.3f; //바닥체크용 박스 크기
    [SerializeField] private float checkerSize2 = 0.3f;
    [SerializeField] private float checkerSize3 = 1.0f; //아마도 이건 건들지 않을 듯.
    [Header("탐색할 레이어")]
    [SerializeField] private LayerMask groundLayers;

    public bool IsCeil { get; private set; } 

    private Vector3 checkerSize; 

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
            IsCeil = true;
        }
        else if (hit == false)
        {
            IsCeil = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (IsCeil == false) //땅에 없는 상태
        {
            Gizmos.color = Color.red;
        }
        else if (IsCeil == true)
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawWireCube(transform.position, new Vector3(checkerSize1, checkerSize2, checkerSize3));
    }
}
