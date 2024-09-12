using ARQuea;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ItemsRef : MonoBehaviour
{
    [SerializeField] public UIDocument uIDocument;
    private VisualElement root;

    Button profil;
    Button home;
    Button search;
    Button connexion;
    Button panier;

    VisualElement imageCont;
    Label nameLab;
    Label priceLab;
    Label sizeLab;

    Button arMode;
    Button ajoutPanier;

    [SerializeField] GameObject itemsUI;
    private ItemsSO currentItem;

    private void OnEnable()
    {
        root = uIDocument.rootVisualElement;

        profil = root.Q<Button>("Profil");
        search = root.Q<Button>("SearchButton");
        home = root.Q<Button>("HomeButton");
        connexion = root.Q<Button>("ConnectButton");
        panier = root.Q<Button>("Panier");

        imageCont = root.Q<VisualElement>("ImageContainer");
        nameLab = root.Q<Label>("NameLab");
        priceLab = root.Q<Label>("PriceLab");
        sizeLab = root.Q<Label>("SizeLab");

        arMode = root.Q<Button>("ARVision");
        ajoutPanier = root.Q<Button>("AjoutPanier");

        List<Button> buttons = new List<Button> { profil, home, search, connexion, panier };
        foreach (var button in buttons)
        {
            button.clickable.clicked += () => OnButtonTouch(button);
        }

        arMode.clickable.clicked += () => OnButtonARClick(arMode);
        ajoutPanier.clickable.clicked += () => OnButtonGoToPanierClick(ajoutPanier);
    }

        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayData(ItemsSO items)
    {
        currentItem = items;

        imageCont.style.backgroundImage = new StyleBackground(items.image);
        nameLab.text = items.name;
        priceLab.text = items.price.ToString() + "€";
        sizeLab.text = items.size.ToString() + "m";
    }

    void OnButtonARClick(Button button)
    {
        itemsUI.SetActive(false);
        SceneManager.LoadScene("ARScene", LoadSceneMode.Additive);
    }

    void OnButtonGoToPanierClick(Button button)
    {
        // Appel pour ajouter l'item au panier
        PanierManager.Instance.AddToPanier(currentItem);
        Debug.Log("selecte :" +  currentItem);
    }

    void OnButtonTouch(Button button)
    {
        UIManager.Instance.OnButtonTouch(button);
    }
}
