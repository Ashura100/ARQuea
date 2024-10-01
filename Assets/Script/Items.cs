using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

        private ItemsSO selectedItem;  // Ajoutez cette variable pour stocker l'item sélectionné

        private void Awake()
        {
            Instance = this;
        }

        public void ConfigureItemButtons(VisualElement root)
        {
            // Récupérez la liste des items depuis le script Search
            List<ItemsSO> items = Search.Instance.items;  // Assurez-vous que Search a une instance statique

            // Recherchez tous les éléments d'interface utilisateur où les items seront affichés
            List<VisualElement> itemButtons = root.Query(className: "Items").ToList();

            // Vérifiez que nous avons suffisamment de boutons pour afficher tous les items
            int itemCount = Mathf.Min(items.Count, itemButtons.Count);

            for (int i = 0; i < itemCount; i++)
            {
                ItemsSO currentItem = items[i];  // Capturez l'élément actuel
                Button button = (Button)itemButtons[i];
                button.text = currentItem.name;  // Utilisez le nom de l'item dans le ScriptableObject
                button.clicked += () => OnItemSelected(currentItem);  // Passez l'élément directement dans le gestionnaire d'événements
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