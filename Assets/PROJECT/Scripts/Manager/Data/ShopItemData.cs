using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData")]
public class ShopItemData : ScriptableObject
{
    public int itemID;
    public Sprite itemIcon;
    public string itemName;
    public int itemPrice;
    public ShopItemType itemType;
}

public enum ShopItemType
{
    character,
    vehicle

}
