using ARQuea;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UIElements;

public class Bathroom : MonoBehaviour
{
    [SerializeField] public UIDocument uIDocument;
    private VisualElement root;

    Button profil;
    Button home;
    Button search;
    Button connexion;
    Button panier;

    [SerializeField] ItemCategory itemCategory;
    private void OnEnable()
    {
        root = uIDocument.rootVisualElement;

        profil = root.Q<Button>("Profil");
        search = root.Q<Button>("SearchButton");
        home = root.Q<Button>("HomeButton");
        connexion = root.Q<Button>("ConnectButton");
        panier = root.Q<Button>("Panier");

        Items.Instance.ConfigureItemButtons(itemCategory, root);

        List<Button> buttons = new List<Button> {profil, home, search, connexion, panier };
        foreach (var button in buttons)
        {
            button.clickable.clicked += () => OnButtonTouch(button);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnButtonTouch(Button button)
    {
        UIManager.Instance.OnButtonTouch(button);
    }
}
