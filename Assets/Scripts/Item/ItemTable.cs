using System.Collections.Generic;
using UnityEngine;

//모든 아이템들의 원본을 보관할 리스트.

public class ItemTable : ScriptableObject
{
    [Header("아이템 등록 리스트")]
    [SerializeField] private List<Item> items;

    //id를 넣으면 Item을 꺼내주는 딕셔너리
    public Dictionary<int, Item> Items;
    

    //딕셔너리를 초기화하는 메서드.
    //별도의 담당자가 호출하는 것으로 한다.
    public void InitializeItemDictionary()
    {
        Items = new Dictionary<int, Item>();

        foreach (Item item in items)
        {
            if(Items.ContainsKey(item.Id))
            {
                Debug.LogWarning("[ItemTable] : 같은 Key로 아이템을 등록 시도하고 있습니다");
                continue;
            }

            Items.TryAdd(item.Id, item);

        }
    }

    public Item GetItem (int id)
    {
        if (Items.TryGetValue(id, out Item item))
        {
            return item;
        }
        else
        {
            Debug.LogWarning($"[ItemTable] : 존재하지 않는 Item Id로 GetItem을 호출했습니다. ID : {id}");
            return null;
        }
    }
}
