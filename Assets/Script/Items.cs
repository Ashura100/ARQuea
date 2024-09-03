using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Device;
using System.Linq;

namespace ARQuea
{
    public class Items : MonoBehaviour
    {
        public static Items Instance;
        private VisualElement root;

        Button home;
        Button search;
        Button connexion;
        Button panier;

        [SerializeField] ItemCategorySO itemCategorySO;
        //ItemsSO[] items ;  // Remplacez GameObject[] par ItemSO[]
        private ItemsSO selectedItem;  // Ajoutez cette variable pour stocker l'item sélectionné

        private void Awake()
        {
            Instance = this;
        }

        public void ConfigureItemButtons(ItemCategory itemCategory, VisualElement root)
        {
            ItemsSO[] items = GetItems(itemCategory);
            List<VisualElement> itemButtons = root.Query(className: "Items").ToList();

            for (int i = 0; i < items.Length; i++)
            {
                ItemsSO currentItem = items[i]; // Capturez l'élément actuel
                Button button = (Button)itemButtons[i];
                button.text = currentItem.name; // Utilisez le nom de l'item dans le ScriptableObject
                button.clicked += () => OnItemSelected(currentItem); // Passez l'élément directement
            }
        }

        public ItemsSO[] GetItems(ItemCategory itemCategory)
        {
            switch (itemCategory)
            {
                case ItemCategory.Room:
                    return itemCategorySO.roomItems;
                    break;
                case ItemCategory.Kitchen:
                    return itemCategorySO.kitchenItems;
                    break;
                case ItemCategory.Chamber:
                    return itemCategorySO.chamberItems;
                    break;
                case ItemCategory.Bathroom:
                    return itemCategorySO.bathroomItems;
                    break;
                default:
                    return itemCategorySO.roomItems.Concat(itemCategorySO.kitchenItems.Concat(itemCategorySO.chamberItems.Concat(itemCategorySO.bathroomItems))).ToArray();
                    break;
            }
        }

        public void OnItemSelected(ItemsSO item)
        {
            selectedItem = item;  // Stockez l'item sélectionné

            if (selectedItem != null)
            {
                Debug.Log("Selected item: " + selectedItem.name);
                UIManager.Instance.ChangeScreen(UIManager.Instance.currentScreen, UIManager.Instance.itemsUI, 0.5f, selectedItem);
                
            }
            else
            {
                Debug.LogWarning("Selected item is null.");
            }
        }

        public ItemsSO GetSelectedItem()
        {
            return selectedItem;  // Ajoutez cette méthode pour récupérer l'item sélectionné
        }
    }
}