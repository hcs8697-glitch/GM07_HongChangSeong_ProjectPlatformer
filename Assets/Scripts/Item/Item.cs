using System;
using UnityEngine;

//WeaponType에서 했던 것처럼, 이 클래스도 추상 클래스로 하고 실제로는 WeaponItem, UseItem 이런 식으로 상속할 것을 고려한다.
public class Item : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;

    public int Id => id;
    public string ItemName => itemName;
    public string ItemDescription => itemDescription;
}
