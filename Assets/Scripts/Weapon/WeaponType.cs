using UnityEngine;

public abstract class WeaponType : ScriptableObject
{


    public abstract void AttackByType(int facingDirection, Team ownerTeam, Transform attackPoint);
}
