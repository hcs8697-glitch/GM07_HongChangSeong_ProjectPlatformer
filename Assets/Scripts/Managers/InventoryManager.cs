
using System.Collections.Generic;
using UnityEngine;

//플레이어의 인벤토리와, 인벤토리에의 접근을 가능하게 할 싱글톤 클래스
//이 클래스가 싱글톤이 되는 게 아니라, GameManager가 싱글톤이게 한다.
public class InventoryManager : MonoBehaviour
{
    //플레이어의 인벤토리. 현재는 이 클래스가 들게끔 한다.
    [Header("플레이어의 인벤토리")]
    [SerializeField] private Inventory inventory;
    [Header("최초 인벤토리 크기")]
    [SerializeField] private int initialCapacity = 20;


    public Inventory Inventory => inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        inventory = new Inventory(initialCapacity);
    }


    public bool TryAddItem(ItemData item)
    {
        bool sucess = inventory.TryAddItem(item);

        if (sucess)
        {
            Debug.Log("아이템 획득 성공");
        }
        else
        {
            Debug.Log("아이템 획득 실패");
        }

        return sucess;
    }

    //슬롯 번호 넣으면 아이템 찾아옴.
    //잘못된 번호의 예외처리는 인벤토리 내부의 메서드에서 처리하고 있음.
    public ItemData GetItem(int slotIndex)
    {
        return inventory.GetItem(slotIndex);
    }

    public bool RemoveItem(int slotIndex)
    {
        return inventory.RemoveItem(slotIndex);
    }

    public void ClearInventory()
    {
        inventory.Clear();
    }
}
