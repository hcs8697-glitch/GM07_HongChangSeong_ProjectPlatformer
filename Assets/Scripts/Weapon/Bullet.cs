using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float lifeTime;
    [SerializeField] private float speed;

    //날아갈 방향
    private int facingDirection;

    public void Initialize(int facingDirection)
    {
        this.facingDirection = facingDirection;
    }


    void Start()
    {
        //삭제 예약
        ReserveDestruction();
    }


    void Update()
    {
        transform.position += Vector3.right * facingDirection * speed * Time.deltaTime;
    }


    //적과 플레이어가 이 스크립트를 공유할 방법을 생각하자.
    //PlayerHealth와 EnemyHealth가 같은 클래스로부터 상속받고,
    //is키워드로 발사한 주체를 판별하여 적용하면 되려나?
    //생성자에서 열거형을 통해 발사주체(Team)을 전달하는 게 좋다고 함.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == 6)
        {
            Destroy(gameObject);
        }

    }

    //TODO : 풀링을 적용할 때 이 안의 메서드만 바꾸기.
    private void ReserveDestruction()
    {
        Destroy(gameObject, lifeTime);
    }
}
