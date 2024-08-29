using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Device;

namespace ARQuea
{
    public class Items : MonoBehaviour
    {
        public static Items Instance;
        [SerializeField] public UIDocument uIDocument;
        private VisualElement root;

        Button home;
        Button search;
        Button connexion;
        Button panier;

        [SerializeField] public ItemsSO[] items;  // Remplacez GameObject[] par ItemSO[]
        private ItemsSO selectedItem;  // Ajoutez cette variable pour stocker l'item sélectionné

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            root = uIDocument.rootVisualElement;

            home = root.Q<Button>("HomeButton");
            search = root.Q<Button>("SearchButton");
            connexion = root.Q<Button>("ConnectButton");
            panier = root.Q<Button>("Panier");

            List<Button> buttons = new List<Button> { home, search, connexion, panier };
            foreach (var button in buttons)
            {
                button.clickable.clicked += () => OnButtonTouch(button);
            }

            // Configuration des boutons d'item
            ConfigureItemButtons();
        }

        private void ConfigureItemButtons()
        {
            List<VisualElement> itemButtons = root.Query(className: "Items").ToList();

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
                UIManager.Instance.ChangeScreen(UIManager.Instance.currentScreen, UIManager.Instance.itemsUI);
                
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

        void OnButtonTouch(Button button)
        {
            UIManager.Instance.OnButtonTouch(button);
        }
    }
}