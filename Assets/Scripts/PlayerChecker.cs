using UnityEngine;

//적의 탐지범위에 플레이어이가 들어있는지 없는지 여부를 isDetected로 반환할 스크립트.
public class PlayerChecker : MonoBehaviour
{
    [SerializeField] private float detectRadius = 1.0f;
    [SerializeField] private LayerMask playerLayer;

    public bool IsDetected { get; private set; } 

    void Update()
    {
        CheckPlayer();
    }

    private void CheckPlayer()
    {      

        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectRadius, playerLayer);

        if (hit == true)
        {
            IsDetected = true;
        }
        else if (hit == false)
        {
            IsDetected = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (IsDetected == false) //땅에 없는 상태
        {
            Gizmos.color = Color.red;
        }
        else if (IsDetected == true)
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
