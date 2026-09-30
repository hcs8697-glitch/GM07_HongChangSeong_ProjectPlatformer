using UnityEngine;

[CreateAssetMenu(fileName = "MeleeType", menuName = "Game Data / Melee Type Weapon")]
public class Melee : WeaponType
{
    [SerializeField] private Vector2 attackOffset;
    [SerializeField] private Vector2 attackSize;
    [SerializeField] private LayerMask damageableLayer;

    public override void Attack(int facingDirection)
    {
        
    }
}
