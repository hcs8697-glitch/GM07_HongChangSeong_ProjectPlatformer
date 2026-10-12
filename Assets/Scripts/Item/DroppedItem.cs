using UnityEngine;

//드롭되어 있는 아이템에 관한 클래스
//필드에 드롭되어 있을 시, 아이템은 위아래로 왕복운동하며 습득 가능함을 플레이어에게 인지시킨다.

public class DroppedItem : MonoBehaviour, IInteractable
{
    [SerializeField] private Item itemData;
    public string Name => itemData.ItemName; //이렇게 하면 ItemData가 만일 null일 때는 어떻게?



    public void Interact()
    {
        
    }
}
