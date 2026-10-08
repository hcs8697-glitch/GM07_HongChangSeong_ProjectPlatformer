using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [Header("아이템 테이블")]
    [SerializeField] private ItemTable itemTable;



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


    public void SpawnItem(int id, Transform position)
    {
        Item newItem = itemTable.GetItem(id);



        //DroppedItem newDroppedItem = Instantiate(droppedItem, position, Quaternion.identity);


        //newDroppedItem.Initialize(newItem);
    }
}
