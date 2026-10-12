
using System.Collections.Generic;
using UnityEngine;

//플레이어의 인벤토리와, 인벤토리에의 접근을 가능하게 할 싱글톤 클래스
//이 클래스가 싱글톤이 되는 게 아니라, GameManager가 싱글톤이게 한다.
public class InventoryManager : MonoBehaviour
{
    //플레이어의 인벤토리. 현재는 이 클래스가 들게끔 한다.
    [Header("플레이어의 인벤토리")]
    [SerializeField] private List<ItemData> inventory = new List<ItemData>();


    public List<ItemData> Inventory => inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(ItemData item)
    {
        //TODO : 가방의 빈 칸을 검사할 수 있도록

        inventory.Add(item);
    }
}
