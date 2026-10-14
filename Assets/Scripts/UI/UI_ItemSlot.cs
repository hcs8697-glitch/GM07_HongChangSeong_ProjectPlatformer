using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image itemImage;

    [Header("이 슬롯이 현재 들고 있는 아이템")]
    [SerializeField] private ItemData itemData;

    [Header("이 슬롯 자체의 버튼")]
    [SerializeField] private Button button;
    

    //이 녀석이 다른 오브젝트인 ItemInfoPanel을 알게 할 방법 : Awake에서 UIInventoryManager를 GetComponentInParent해와야 함.
    //UIInventoryManager의 필드에 Info패널을 넣으면 됨.




    //슬롯의 정보 초기화
    public void Initialize(ItemData data)
    {
        this.itemData = data;
        nameText.text = data.name; //data.ItemName;으로 해야 됨
        itemImage.sprite = data.ItemSprite;
    }

    //슬롯을 눌렀을 때 무언가 실행시킬 메서드
    //플레이어가 무기가 있으면 장비, 없으면 해제 이렇게 하고 싶은데 이거 어떻게 할지?
    public void OnClickedButton()
    {
        //GameManager.Instance.Equipment.Equip(itemData);
        GameManager.Instance.Equipment.SwitchWeapon(itemData);
    }

    //아이템 슬롯에 마우스를 댔을 때 무언가 실행시킬 메서드
    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    //아이템 슬롯에서 마우스를 땠을 때 무언가 실행시킬 메서드
    public void OnPointerExit(PointerEventData eventData)
    {

    }


}
