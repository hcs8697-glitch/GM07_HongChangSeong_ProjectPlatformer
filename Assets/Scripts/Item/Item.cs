using System;
using UnityEngine;


public class Item : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private string itemName;

    public int Id => id;
    public string ItemName => itemName;
}
