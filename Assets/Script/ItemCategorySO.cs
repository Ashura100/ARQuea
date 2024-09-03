using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemCategorySo")]
public class ItemCategorySO : ScriptableObject
{
    public ItemsSO[] roomItems;
    public ItemsSO[] kitchenItems;
    public ItemsSO[] chamberItems;
    public ItemsSO[] bathroomItems;
}

public enum ItemCategory
{
    Room,
    Kitchen,
    Chamber,
    Bathroom,
    All
}
