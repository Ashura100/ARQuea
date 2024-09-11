using ARQuea;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Card : MonoBehaviour
{
    [SerializeField] public UIDocument uIDocument;
    private VisualElement root;

    Button profil;
    Button home;
    Button search;
    Button connexion;

    Button pay;

    VisualElement cardContainer;

    // ListView pour afficher les items du panier
    ListView listViewPanier;

    private void OnEnable()
    {
        root = uIDocument.rootVisualElement;

        profil = root.Q<Button>("Profil");
        home = root.Q<Button>("HomeButton");
        search = root.Q<Button>("SearchButton");
        connexion = root.Q<Button>("ConnectButton");

        pay = root.Q<Button>("Pay");

        // Initialisation du container du ListView
        cardContainer = root.Q<VisualElement>("CardContainer");
        cardContainer.style.display = DisplayStyle.None; // Cache par défaut

        // Initialisation du ListView
        listViewPanier = new ListView();
        listViewPanier.style.flexGrow = 1;

        // Ajout du ListView au conteneur
        cardContainer.Add(listViewPanier);

        List<Button> buttons = new List<Button> { profil, home, search, connexion };
        foreach (var button in buttons)
        {
            button.clickable.clicked += () => OnButtonTouch(button);
        }

        UpdatePanier(PanierManager.Instance.panierItems);
    }

    // Méthode pour mettre à jour et afficher le panier
    public void UpdatePanier(List<ItemsSO> panierItems)
    {
        listViewPanier.itemsSource = panierItems;
        listViewPanier.makeItem = () => new Label();
        listViewPanier.bindItem = (element, i) =>
        {
            (element as Label).text = $"{panierItems[i].objectName} - {panierItems[i].price}€";
        };
        listViewPanier.Rebuild();

        // Affiche le container du panier
        cardContainer.style.display = DisplayStyle.Flex;
    }

    void OnButtonTouch(Button button)
    {
        UIManager.Instance.OnButtonTouch(button);
    }
}
