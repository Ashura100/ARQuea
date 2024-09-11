using ARQuea;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UIElements;

public class Search : MonoBehaviour
{
    [SerializeField] public UIDocument uIDocument;
    private VisualElement root;

    TextField searchText;
    Button searchEnter;
    Button kitchen;
    Button room;
    Button chamber;
    Button bathroom;

    Button profil;
    Button home;
    Button connexion;
    Button panier;

    private void OnEnable()
    {
        root = uIDocument.rootVisualElement;

        searchEnter = root.Q<Button>("SearchEnter");
        profil = root.Q<Button>("Profil");
        searchText = root.Q<TextField>("Search");
        kitchen = root.Q<Button>("Kitchen");
        room = root.Q<Button>("Room");
        chamber = root.Q<Button>("Chamber");
        bathroom = root.Q<Button>("Bathroom");

        home = root.Q<Button>("HomeButton");
        connexion = root.Q<Button>("ConnectButton");
        panier = root.Q<Button>("Panier");

        List<Button> categoryButtons = new List<Button> { kitchen, room, chamber, bathroom };
        foreach (var button in categoryButtons)
        {
            button.clickable.clicked += () => OnButtonTouch(button);
        }

        List<Button> buttons = new List<Button> { profil, home, connexion, panier };
        foreach (var button in buttons)
        {
            button.clickable.clicked += () => OnButtonTouch(button);
        }

        searchText.RegisterValueChangedCallback(evt => OnSearchTextChanged(evt.newValue));

        searchEnter.clickable.clicked += () => OnButtonEnterClick(searchEnter);
    }

    private void OnSearchTextChanged(string newValue)
    {
        SearchItems(newValue);
    }

    public List<ItemsSO> SearchItems(string searchTextValue)
    {
        ItemsSO[] items = Items.Instance.GetItems(ItemCategory.All);

        // Créer une liste pour stocker les résultats de recherche
        List<ItemsSO> foundItems = new List<ItemsSO>();

        // Itérer sur tous les items et vérifier si leur nom contient le texte de recherche
        foreach (ItemsSO item in items)
        {
            // Comparer le texte de recherche et le nom de l'item en mode insensible à la casse
            if (item.name.Equals(searchTextValue, System.StringComparison.OrdinalIgnoreCase))
            {
                foundItems.Add(item);
            }
        }

        return foundItems;
    }

    // Modification ici pour utiliser la valeur du champ de recherche et effectuer le changement d'écran
    void OnButtonEnterClick(Button button)
    {
        // Utilise la valeur actuelle du champ de recherche
        string searchValue = searchText.value;

        // Appelle SearchItems pour obtenir les résultats de recherche
        List<ItemsSO> foundItems = SearchItems(searchValue);

        // Affiche l'item si trouvé, sinon affiche un message d'erreur
        if (foundItems.Count > 0)
        {
            ItemsSO foundItem = foundItems[0]; // On utilise le premier item trouvé
            Debug.Log("Found item: " + foundItem.name);

            // On récupère le prefab du foundItem et on l'affiche dans itemsUI
            GameObject itemPrefab = foundItem.gameOject; // Assurez-vous que le prefab est bien référencé

            // Appelle ChangeScreen pour afficher itemsUI
            UIManager.Instance.ChangeScreen(UIManager.Instance.currentScreen, UIManager.Instance.itemsUI, 0.5f, foundItem);
        }
        else
        {
            Debug.Log("No items found matching: " + searchValue);
        }
    }

    void OnButtonTouch(Button button)
    {
        UIManager.Instance.OnButtonTouch(button);
    }
}
