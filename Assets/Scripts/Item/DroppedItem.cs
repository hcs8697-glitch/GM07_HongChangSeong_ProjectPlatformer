using UnityEngine;

//드롭되어 있는 아이템에 관한 클래스
//필드에 드롭되어 있을 시, 아이템은 위아래로 왕복운동하며 습득 가능함을 플레이어에게 인지시킨다.
//프리팹은 일반 사각형으로 만들고, 생성됐을 때 외관을 전환하도록 한다.

[RequireComponent(typeof(Collider2D))]
public class DroppedItem : MonoBehaviour, IInteractable
{
    [Header("전달할 아이템")]
    [SerializeField] private ItemData itemData;
    [SerializeField] private SpriteRenderer spriteRenderer;
    //public string Name => itemData.ItemName; //이렇게 하면 ItemData가 만일 null일 때는 어떻게?

    //데이터가 없으면 빈칸, 있으면 그 데이터의 이름 반환
    public string Name => itemData != null
    ? itemData.ItemName
    : string.Empty;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(ItemData data)
    {
        this.itemData = data;
        spriteRenderer.sprite = data.ItemSprite;
    }

    public void Interact()
    {
        if(itemData == null)
        {
            Debug.Log("[DroppedItem] : 데이터가 null입니다");
            return;
        }

        if (GameManager.Instance.Inventory.TryAddItem(itemData))
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("아이템 습득에 실패했습니다.");
        }
    }
}
