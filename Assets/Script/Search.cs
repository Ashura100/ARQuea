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
