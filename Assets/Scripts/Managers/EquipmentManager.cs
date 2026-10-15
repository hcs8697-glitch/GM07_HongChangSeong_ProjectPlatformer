using System;
using UnityEngine;

//플레이어의 장비를 변경할 클래스
//문제는, 결국 얘 정도는 플레이어를 알아야 함.

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] private Player player;

    public event Action<Weapon> weaponChanged;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //전달받은 장비로 플레이어에게 장착시키는 메서드
    //얘가 이미 플레이어를 알고 있잖아?
    //그러니까 얘가 검사를 하는 거지


    //플레이어의 무기를 변경하는 메서드
    //장착을 해제하는 건 이 메서드 이외의 방식에서 한다.
    public void SwitchWeapon(ItemData data)
    {
        if(player == null)
        {
            player = FindAnyObjectByType<Player>();
            Debug.Log("[EquipmentManager] : 플레이어가 인스펙터에서 할당되지 않아, 검색해 찾았습니다.");
        }


        //현재 플레이어가 무기를 장착하고 있지 않다면, 전달받은 무기를 장착한다
        if (player.Attack.Weapon == null)
        {
            weaponChanged?.Invoke(data.Weapon);
        }
        //현재 플레이어가 전달받은 무기와 다른 무기를 장착하고 있으면 전달받은 무기로 장착한다
        else if (player.Attack.Weapon != data.Weapon)
        {
            weaponChanged?.Invoke(null);
            weaponChanged?.Invoke(data.Weapon);
        }
        else if (player.Attack.Weapon == data.Weapon) //임시. 전달받은 무기와 동일한 무기를 장착하고 있다면 해제시킨다
        {
            weaponChanged?.Invoke(null);
        }
    }


    public void EquipWeapon(ItemData data)
    {
        if (data == null || player == null) return;

        weaponChanged?.Invoke(data.Weapon);
    }

    //장비 해제 의뢰 메서드
    public void UnEquipWeapon()
    {
        if (player == null) return;

        weaponChanged?.Invoke(null);
    }
}
