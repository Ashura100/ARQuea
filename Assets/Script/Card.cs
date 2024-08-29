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

    private void OnEnable()
    {
        root = uIDocument.rootVisualElement;

        profil = root.Q<Button>("Profil");
        home = root.Q<Button>("HomeButton");
        search = root.Q<Button>("SearchButton");
        connexion = root.Q<Button>("ConnectButton");

        List<Button> buttons = new List<Button> { profil, home, search, connexion };
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
