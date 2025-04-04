using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemList", menuName = "Scriptable Objects/ItemList")]

public class ItemList : ScriptableObject
{
    public Item[] items;
}
[Serializable]
public class Item
{
    public string itemName;
    public Sprite image;
}