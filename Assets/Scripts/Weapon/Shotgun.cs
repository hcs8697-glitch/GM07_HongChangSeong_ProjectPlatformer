using UnityEngine;

[CreateAssetMenu(fileName = "RangeType", menuName = "Game Data / Shotgun Type Weapon")]
public class Shotgun : WeaponType
{
    [Header("1회에 발사할 갯수")]
    [Min(1)]
    [SerializeField] private int bulletCount;

    [Header("산탄도")]
    [SerializeField] private float spread;

    [Header("발사할 총알")]
    [SerializeField] private Bullet bullet;

    /*
    뭔가 업그레이드 등을 통해 총알 갯수 변경하고 싶거나, 다른 클래스에서 1 미만으로 변경하지 못하게 하려면 이렇게 해야 할 듯?
    public int BulletCount
    {
        get => bulletCount;
        set => bulletCount = Mathf.Max(1, value);
    }
    */

    //Team과 바라보는 방향을 전달하기 위해서 이 녀석이 알 필요가 없다. 이 메서드는 다른 클래스에서 호출할 것이므로.
    //그리고, FacingDirection이 int형이므로 총알은 무조건 상하좌우로만 나가게 된다. float으로 변경해볼 가치는 있을 듯.
    public override void AttackByType(int facingDirection, Team ownerTeam, Transform attackPoint)
    {
        if (bullet == null)
        {
            Debug.Log($"[Shotgun] : {this}에 Bullet이 등록되지 않았습니다.");
            return;
        }

        for (int i = 0; i < bulletCount; i++)
        {
            //등록한 총알의 복제본을 만들고 초기화를 한다. 매개변수로 전달받은 Transform의 위치와 회전값을 이용한다.
            Bullet newBullet = Instantiate(bullet, attackPoint.position, attackPoint.rotation);

            //Spread를 일단 1로 주면, 맨 위의 총알은 1, 맨 아래의 총알은 -1이 되어야 한다.
            //일단 5발 기준으로 한다면 for문을 돌 때마다 1, 0.5, 0, -0.5, -1이 되어야겠지?
            //i는 0부터 4까지 돌텐데, 계산식은 spread - i * 0.4
            float finalSpread = spread - (2f * spread / (bulletCount - 1)) * i;

            //초기화할 때 -spread를 해준다면 총알의 산탄도를 구현할 수 있을 것.
            newBullet.Initialize(facingDirection, ownerTeam, finalSpread);



        }

    }
}
