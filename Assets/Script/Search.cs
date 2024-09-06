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
    }

    private void OnSearchTextChanged(string newValue)
    {
        SearchItems(newValue);
    }

    public void SearchItems(string searchTextValue)
    {
        Debug.Log(Items.Instance);
        ItemsSO[] items = Items.Instance.GetItems(ItemCategory.All);

        // Créer une liste pour stocker les résultats de recherche
        List<ItemsSO> foundItems = new List<ItemsSO>();

        // Itérer sur tous les items et vérifier si leur nom contient le texte de recherche
        foreach (ItemsSO item in items)
        {
            if (item.name.ToLower().Contains(searchTextValue.ToLower()))
            {
                foundItems.Add(item);
            }
        }

        // Afficher les résultats ou traiter la liste des éléments trouvés
        if (foundItems.Count > 0)
        {
            foreach (ItemsSO foundItem in foundItems)
            {
                Debug.Log("Found item: " + foundItem.name);

                GameObject itemFounded = Instantiate(foundItem.gameOject); // Si foundItem a un prefab associé

                // Ensuite, on peut appeler ChangeScreen avec l'item trouvé converti en GameObject
                //UIManager.Instance.ChangeScreen(UIManager.Instance.currentScreen, UIManager.Instance.itemsUI, 0.5f, itemFounded);
            }
        }
        else
        {
            Debug.Log("No items found matching: " + searchTextValue);
        }
    }

    void OnButtonTouch(Button button)
    {
        UIManager.Instance.OnButtonTouch(button);
    }
}
