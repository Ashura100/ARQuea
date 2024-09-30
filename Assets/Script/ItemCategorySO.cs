using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemCategorySo")]
public class ItemCategorySO : ScriptableObject
{
    public string currentCategory;

    public ItemCategorySO(string category) => currentCategory = category;

    public ItemsSO[] roomItems;
    public ItemsSO[] kitchenItems;
    public ItemsSO[] chamberItems;
    public ItemsSO[] bathroomItems;
}
