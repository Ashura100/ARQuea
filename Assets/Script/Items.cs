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
        [SerializeField] GameObject itemsUI;
        private VisualElement root;

        Button home;
        Button search;
        Button connexion;
        Button panier;

        [SerializeField] public ItemsSO[] items;  // Remplacez GameObject[] par ItemSO[]
        private ItemsSO selectedItem;  // Ajoutez cette variable pour stocker l'item s�lectionn�

        private void Awake()
        {
            Instance = this;

            // Configuration des boutons d'item
            ConfigureItemButtons();
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
        }

        private void ConfigureItemButtons()
        {
            List<VisualElement> itemButtons = root.Query(className: "Item").ToList();

            for (int i = 0; i < items.Length; i++)
            {
                ItemsSO currentItem = items[i]; // Capturez l'�l�ment actuel
                Button button = (Button)itemButtons[i];
                button.text = currentItem.name; // Utilisez le nom de l'item dans le ScriptableObject
                button.clicked += () => OnItemSelected(currentItem); // Passez l'�l�ment directement
            }
        }

        public void OnItemSelected(ItemsSO item)
        {
            selectedItem = item;  // Stockez l'item s�lectionn�

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
            return selectedItem;  // Ajoutez cette m�thode pour r�cup�rer l'item s�lectionn�
        }

        void OnButtonTouch(Button button)
        {
            UIManager.Instance.OnButtonTouch(button);
        }
    }
}