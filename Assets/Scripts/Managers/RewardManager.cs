using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [Header("아이템 테이블")]
    [SerializeField] private ItemTable itemTable;

    [Header("필드 드랍 아이템 프리팹")]
    [SerializeField] private DroppedItem droppedItem;


    private void Awake()
    {
        if(itemTable == null)
        {
            Debug.Log("[RewardManager] : 아이템 테이블이 없습니다.");
            enabled = false;
        }

        itemTable.InitializeItemDictionary();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void SpawnItemByID(int id, Transform position)
    {
        ItemData newItem = itemTable.GetItem(id);



        //DroppedItem newDroppedItem = Instantiate(droppedItem, position, Quaternion.identity);


        //newDroppedItem.Initialize(newItem);
    }

    public void SpawnItemBySO (ItemData item, Vector3 position)
    {
        ItemData newItem = item;

        DroppedItem newDroppedItem = Instantiate(droppedItem, position, Quaternion.identity);


        newDroppedItem.Initialize(newItem);

    }
}
