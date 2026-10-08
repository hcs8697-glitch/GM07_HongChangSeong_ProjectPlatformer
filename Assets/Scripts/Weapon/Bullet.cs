using UnityEngine;

//총알의 역할을 수행할 오브젝트에게 붙여 사용한다.
[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [Header("총알 스탯")]
    [SerializeField] private int damage;
    [SerializeField] private float lifeTime;
    [SerializeField] private float speed;

    [Header("건들 필요 없는 소속")]
    [SerializeField] private Team ownerTeam;

    [Header("총알이 삭제될 레이어")]
    [SerializeField] private LayerMask bulletDestroyLayer;

    //날아갈 방향
    private int facingDirection;
    private float spread;

    //초기화 : 총알이 생성될 때 방향과, 누구 소유인지를 결정한다.
    public void Initialize(int facingDirection, Team ownerTeam, float spread)
    {
        this.facingDirection = facingDirection;
        this.ownerTeam = ownerTeam;
        this.spread = spread;
    }


    void Start()
    {
        //삭제 예약
        ReserveDestruction();
    }


    void Update()
    {
        //transform.position += Vector3.right * facingDirection * speed * Time.deltaTime;
        transform.position += new Vector3(1 * facingDirection, spread, 1) * speed * Time.deltaTime;
    }


    //적과 플레이어가 이 스크립트를 공유할 방법을 생각하자.
    //PlayerHealth와 EnemyHealth가 같은 클래스로부터 상속받고,
    //is키워드로 발사한 주체를 판별하여 적용하면 되려나?
    //생성자에서 열거형을 통해 발사주체(Team)을 전달하는 게 좋다고 함.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("총알이 무엇과 충돌함");
        if (collision.TryGetComponent<IDamageable>(out IDamageable target))
        {
            //IDamageable을 검출했고, 소속이 다를 경우에만 피해를 입힌다.
         
            if (target.Team != this.ownerTeam)
            {
                Debug.Log("총알이 피해를 입힘");
                target.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        /*
        //현재 6번 레이어인 "Ground"에 총알이 부딪히면 삭제된다.
        //다만, 다른 프로젝트에서도 이 스크립트를 사용하기 위해서는 반드시 6번 레이어가 Ground여야 할 필요가 있다. 추후 개선 필요.
        //비교 방식이 잘못되어 동작하지 않는데, 이 부분은 어떻게 처리할지 고민 필요. 비트연산 때문에 안 되는 것 같다는데.
        if (collision.gameObject.layer == bulletDestroyLayer) 
        {
            Debug.Log("총알이 벽과 충돌함");
            Destroy(gameObject);
        }
        */
        else
        {
            Debug.Log("총알이 피해를 줄 수 없는 대상과 충돌함");
            Destroy(gameObject);
        }

    }

    //TODO : 풀링을 적용할 때 이 안의 메서드만 바꾸기.
    private void ReserveDestruction()
    {
        Destroy(gameObject, lifeTime);
    }
}
