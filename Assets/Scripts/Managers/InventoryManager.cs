
using System.Collections.Generic;
using UnityEngine;

//플레이어의 인벤토리와, 인벤토리에의 접근을 가능하게 할 싱글톤 클래스
//이 클래스가 싱글톤이 되는 게 아니라, GameManager가 싱글톤이게 한다.
public class InventoryManager : MonoBehaviour
{
    [Header("플레이어의 인벤토리")]
    [SerializeField] private List<Item> inventory = new List<Item>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(Item item)
    {
        //TODO : 가방의 빈 칸을 검사할 수 있도록

        inventory.Add(item);
    }
}
