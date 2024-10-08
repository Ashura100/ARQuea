using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

namespace ARQuea
{
    public class Profil : MonoBehaviour
    {
        [SerializeField] public UIDocument uIDocument;
        private VisualElement root;
        Label role;
        Label username;
        Label mail;
        Button home;
        Button search;
        Button connexion;
        Button panier;

        private void OnEnable()
        {
            root = uIDocument.rootVisualElement;
            username = root.Q<Label>("Username");
            mail = root.Q<Label>("Mail");
            role = root.Q<Label>("Role");
            home = root.Q<Button>("Home");
            search = root.Q<Button>("SearchButton");
            connexion = root.Q<Button>("ConnectButton");
            panier = root.Q<Button>("Panier");

            List<Button> buttons = new List<Button> { home, search, connexion, panier };
            foreach (var button in buttons)
            {
                button.clickable.clicked += () => OnButtonTouch(button);
            }

            DisplayData();
        }

        void Start()
        {
            
        }

        void DisplayData()
        {
            if (LogManager.Instance.currentUser != null)
            {
                username.text = LogManager.Instance.currentUser.username;
                mail.text = LogManager.Instance.currentUser.email;
                role.text = LogManager.Instance.currentUser.role;
            }
            else
            {
                Debug.LogWarning("Aucun utilisateur connecté.");
                username.text = "Non connecté";
                mail.text = "Non connecté";
                role.text = "Non connecté";
            }
        }

        void OnButtonTouch(Button button)
        {
            UIManager.Instance.OnButtonTouch(button);
        }
    }

}
