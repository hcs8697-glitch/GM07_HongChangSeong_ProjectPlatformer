using System;
using UnityEngine;

//WeaponType에서 했던 것처럼, 이 클래스도 추상 클래스로 하고 실제로는 WeaponItem, UseItem 이런 식으로 상속할 것을 고려한다.
[CreateAssetMenu(fileName = "NewItem", menuName = "Game Data / Item")]
public class ItemData : ScriptableObject
{
    [Header("고유 id")]
    [SerializeField] private int id;
    [Header("아이템 이름")]
    [SerializeField] private string itemName;
    [Header("아이템 설명")]
    [SerializeField] private string itemDescription;
    [Header("아이템 외형")]
    [SerializeField] private GameObject itemPrefab;
    [Header("아이템 타입")]
    [SerializeField] private ItemType itemType;
    [Header("(무기일 경우만) 생성할 무기")]
    [SerializeField] private Weapon weapon;
    [Header("무기 아이콘")]
    [SerializeField] private Sprite itemSprite;
    

    public int Id => id;
    public string ItemName => itemName;
    public string ItemDescription => itemDescription;
    public GameObject ItemPrefab => itemPrefab;
    public ItemType ItemType => itemType;
    public Weapon Weapon => weapon;
    

    public Sprite ItemSprite => itemSprite;
}
