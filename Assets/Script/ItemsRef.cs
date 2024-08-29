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

    Button arMode;

    VisualElement imageCont;
    Label nameLab;
    Label priceLab;
    Label sizeLab;

    [SerializeField] GameObject itemsUI;

    private void OnEnable()
    {
        root = uIDocument.rootVisualElement;

        profil = root.Q<Button>("Profil");
        search = root.Q<Button>("SearchButton");
        home = root.Q<Button>("HomeButton");
        connexion = root.Q<Button>("ConnectButton");
        panier = root.Q<Button>("Panier");

        arMode = root.Q<Button>("ARVision");

        imageCont = root.Q<VisualElement>("ImageContainer");
        nameLab = root.Q<Label>("NameLab");
        priceLab = root.Q<Label>("PriceLab");
        sizeLab = root.Q<Label>("SizeLab");

        List<Button> buttons = new List<Button> { profil, home, search, connexion, panier };
        foreach (var button in buttons)
        {
            button.clickable.clicked += () => OnButtonTouch(button);
        }

        arMode.clickable.clicked += () => OnButtonARClick(arMode);
    }

        // Start is called before the first frame update
    void Start()
    {
        DisplayData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayData()
    {
        ItemsSO selectedItem = Items.Instance.GetSelectedItem();

        //imageCont.style.backgroundImage = selectedItem.image;
        nameLab.text = selectedItem.name;
        priceLab.text = selectedItem.price.ToString() + "€";
        sizeLab.text = selectedItem.size.ToString() + "M";
    }

    void OnButtonARClick(Button button)
    {
        itemsUI.SetActive(false);
        SceneManager.LoadScene("ARScene", LoadSceneMode.Additive);
    }
    void OnButtonTouch(Button button)
    {
        UIManager.Instance.OnButtonTouch(button);
    }
}
