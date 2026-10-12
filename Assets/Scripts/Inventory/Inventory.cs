using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor.Search;
using System.Runtime.CompilerServices;


//아이템의 데이터를 갖고 있는 인벤토리

[Serializable]
public class Inventory
{
    [Header("인벤토리 현환")]
    [SerializeField] private List<ItemData> inventory;
    [Header("인벤토리 최대용량")]
    [SerializeField] private int capacity;

    public int Capacity => capacity;
    public int ItemCount => inventory.Count;

    //인벤토리가 찼는지 안 찼는지 판별함.
    public bool IsFull => inventory.Count >= capacity;


    //인벤토리가 생성될 때, 최대 용량을 필요로 한다.

    public Inventory(int capacity)
    {
        this.capacity = capacity;
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
        Debug.Log($"[Inventory] 성공적으로 {slotIndex}의 아이템 {inventory[slotIndex].name}을 제거했습니다.");
        return true;
    }
  
}
