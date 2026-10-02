using UnityEngine;

//총알을 발사하는 무기에게 넣을 SO 에셋을 생성하는 스크립트
//발사 규칙 : 발사할 수량, 발사할 총알, 발사할 각도 등을 필드로 갖는다.

[CreateAssetMenu(fileName = "RangeType", menuName = "Game Data / Range Type Weapon")]
public class Range : WeaponType
{
    //발 당 피해량은 총알에서 결정하는 게 맞지 않을까.
    [Header("1회에 발사할 갯수")]
    [SerializeField] private int bulletCount;

    [Header("산탄도")]
    [SerializeField] private float spread;

    [Header("발사할 총알")]
    [SerializeField] private Bullet bullet;

    //Team과 바라보는 방향을 전달하기 위해서 이 녀석이 알 필요가 없다. 이 메서드는 다른 클래스에서 호출할 것이므로.
    //그리고, FacingDirection이 int형이므로 총알은 무조건 상하좌우로만 나가게 된다. float으로 변경해볼 가치는 있을 듯.
    public override void AttackByType(int facingDirection, Team ownerTeam, Transform attackPoint)
    {
        if(bullet == null)
        {
            Debug.Log("[Range] : Bullet이 등록되지 않았습니다.");
            return;
        }
         
        for (int i = 0;  i < bulletCount; i++)
        {
            //등록한 총알의 복제본을 만들고 초기화를 한다. 매개변수로 전달받은 Transform의 위치와 회전값을 이용한다.
            Bullet newBullet = Instantiate(bullet, attackPoint.position, attackPoint.rotation);

            //초기화할 때 -spread를 해준다면 총알의 산탄도를 구현할 수 있을 것.
            newBullet.Initialize(facingDirection, ownerTeam);


            //bullet.Initialize(facingDirection, ownerTeam);
            //Instantiate(bullet);
        }

    }
}
