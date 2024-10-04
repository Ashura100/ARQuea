using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ARQuea
{
    public class Bathroom : MonoBehaviour
    {
        [SerializeField] public UIDocument uIDocument;
        private VisualElement root;

        Button profil;
        Button home;
        Button search;
        Button connexion;
        Button panier;

        private void OnEnable()
        {
            root = uIDocument.rootVisualElement;

            profil = root.Q<Button>("Profil");
            search = root.Q<Button>("SearchButton");
            home = root.Q<Button>("HomeButton");
            connexion = root.Q<Button>("ConnectButton");
            panier = root.Q<Button>("Panier");

            Items.Instance.ConfigureItemButtons(root);

            List<Button> buttons = new List<Button> { profil, home, search, connexion, panier };
            foreach (var button in buttons)
            {
                button.clickable.clicked += () => OnButtonTouch(button);
            }
        }

        void OnButtonTouch(Button button)
        {
            UIManager.Instance.OnButtonTouch(button);
        }
    }

}