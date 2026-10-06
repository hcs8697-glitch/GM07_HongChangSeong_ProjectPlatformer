using System;
using UnityEngine;

//테스트로 등록된 무기를 플레이어에게 장착시키는 클래스
//추후 인벤토리 등으로 발전을 고려한다.


public class TestWeaponEquiper : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private Player player;

    public event Action<Weapon> weaponChanged;

  
    public void EquipWeapon()
    {
        if (weapon == null || player == null) return;

        weaponChanged?.Invoke(weapon);
    }

    public void UnEquipWeapon()
    {
        if (weapon == null || player == null) return;

        weaponChanged?.Invoke(null);
    }
}
