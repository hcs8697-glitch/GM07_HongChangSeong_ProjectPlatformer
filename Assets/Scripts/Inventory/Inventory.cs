using System.Collections.Generic;
using System;
using UnityEngine;



//아이템의 데이터를 갖고 있는 인벤토리

[Serializable]
public class Inventory
{
    [Header("인벤토리 현황")]
    [SerializeField] private List<ItemData> inventory;
    
    //인벤토리 용량. 생성 시에는 이 값을 변경하는 게 아니라 
    private int capacity;

    public int Capacity => capacity;
    public int ItemCount => inventory.Count;

    //인벤토리가 찼는지 안 찼는지 판별함.
    public bool IsFull => inventory.Count >= capacity;


    //인벤토리가 생성될 때, 최대 용량을 필요로 한다.

    public Inventory(int capacity)
    {
        //인벤토리가 생성될 때, 칸은 최소한 1로 생성된다.
        this.capacity = Mathf.Max(1, capacity);
        inventory = new List<ItemData>(capacity);
    }

    public bool TryAddItem(ItemData item)
    {
        if (item == null || IsFull)
        {
            Debug.Log("[Inventory] 가방이 가득 찼거나 존재하지 않는 아이템을 추가 시도중입니다.");
            return false;
        }

        inventory.Add(item);
        return true;
    }

    public ItemData GetItem(int slotIndex)
    {
        if(slotIndex < 0 || slotIndex >= inventory.Count)
        {
            Debug.Log("[Inventory] 잘못된 인덱스로 아이템을 찾고 있습니다.");
            return null;
        }

        return inventory[slotIndex];
    }

    public bool RemoveItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= inventory.Count)
        {
            Debug.Log("[Inventory] 잘못된 인덱스로 아이템을 찾고 있습니다.");
            return false;
        }

        inventory.RemoveAt(slotIndex);
        Debug.Log($"[Inventory] 성공적으로 {slotIndex}의 아이템을 제거했습니다.");
        return true;
    }

    public void Clear()
    {
        inventory.Clear();
    }
  
}
