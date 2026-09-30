using UnityEngine;

[CreateAssetMenu(fileName = "RangeType", menuName = "Game Data / Range Type Weapon")]
public class Range : WeaponType
{
    //발 당 피해량은 총알에서 결정하는 게 맞지 않을까.
    [Header("1회에 발사할 갯수")]
    [SerializeField] private int bulletCount;

    [Header("발사할 총알")]
    [SerializeField] private Bullet bullet;


    public override void Attack(int facingDirection)
    {
        if(bullet == null)
        {
            Debug.Log("[Range] : Bullet이 등록되지 않았습니다.");
            return;
        }
        
        for (int i = 0;  i < bulletCount; i++)
        {

            bullet.Initialize(facingDirection);
            Instantiate(bullet);
        }

    }
}
