using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ARQuea
{
    public class Home : MonoBehaviour
    {
        [SerializeField] public UIDocument uIDocument;
        private VisualElement root;

        Button profil;
        Button search;
        Button connexion;
        Button panier;

        private void OnEnable()
        {
            root = uIDocument.rootVisualElement;

            profil = root.Q<Button>("Profil");
            search = root.Q<Button>("SearchButton");
            connexion = root.Q<Button>("ConnectButton");
            panier = root.Q<Button>("Panier");

            List<Button> buttons = new List<Button> { profil, search, connexion, panier };
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

}