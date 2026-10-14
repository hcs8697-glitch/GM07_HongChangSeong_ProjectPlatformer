
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryManager : MonoBehaviour
{
    [Header("인벤토리 UI")]
    [SerializeField] private RectTransform inventoryUI;

    [Header("아이템 슬롯 원본")]
    [SerializeField] private UI_ItemSlot slotPrefab;

    [Header("아이템 슬롯이 생성될 부모")]
    [SerializeField] private RectTransform content;

    [Header("슬롯 복사본 리스트")]
    [SerializeField] private List<UI_ItemSlot> copiedPrefabs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public void ToggleInventory(bool toggle)
    {
        if (inventoryUI == null)
        {
            Debug.LogWarning("[UIInventoryManager] : 인벤토리가 필드에 등록되지 않았습니다");
            return;
        }

        inventoryUI.gameObject.SetActive(toggle);

        switch(toggle)
        {
            case true:
                RefreshItemList();
                break;
            case false:
                DisableItemList();
                break;
        }

    }



    public void RefreshItemList()
    {
        Inventory itemList = GameManager.Instance.Inventory.Inventory;

        for (int i = 0; i < itemList.ItemCount; i++)
        {
            UI_ItemSlot slot = Instantiate(slotPrefab, content);

            slot.Initialize(itemList.GetItem(i));

            copiedPrefabs.Add(slot);
        }


    }

    public void DisableItemList()
    {
        for (int i = 0; i <copiedPrefabs.Count; i++)
        {
            Destroy(copiedPrefabs[i].gameObject);
        }

        copiedPrefabs.Clear();
    }

    

}


