using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace ARQuea
{
    public class Items : MonoBehaviour
    {
        public static Items Instance;
        [SerializeField] public UIDocument uIDocument;
        [SerializeField] GameObject itemsUI;
        private VisualElement root;
        [SerializeField] public ItemsSO[] items;  // Remplacez GameObject[] par ItemSO[]

        private ItemsSO selectedItem;  // Ajoutez cette variable pour stocker l'item sélectionné

        private void Awake()
        {
            Instance = this;
            root = uIDocument.rootVisualElement;

            // Configuration des boutons d'item
            ConfigureItemButtons();
        }

        private void ConfigureItemButtons()
        {
            List<VisualElement> itemButtons = root.Query(className: "Item").ToList();

            for (int i = 0; i < items.Length; i++)
            {
                ItemsSO currentItem = items[i]; // Capturez l'élément actuel
                Button button = (Button)itemButtons[i];
                button.text = currentItem.name; // Utilisez le nom de l'item dans le ScriptableObject
                button.clicked += () => OnItemSelected(currentItem); // Passez l'élément directement
            }
        }

        public void OnItemSelected(ItemsSO item)
        {
            selectedItem = item;  // Stockez l'item sélectionné

            if (selectedItem != null)
            {
                Debug.Log("Selected item: " + selectedItem.name);
                itemsUI.SetActive(false);
                SceneManager.LoadScene("ARScene", LoadSceneMode.Additive);
                
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