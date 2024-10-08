using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ARQuea
{
    public class Login : MonoBehaviour
    {
        [SerializeField] public UIDocument uIDocument;
        private VisualElement root;

        Button home;
        Button profil;
        Button search;
        Button connexion;
        Button panier;

        private TextField username;
        private TextField password;
        private Button logInButton;

        private void OnEnable()
        {
            root = uIDocument.rootVisualElement;

            home = root.Q<Button>("HomeButton");
            profil = root.Q<Button>("Profil");
            search = root.Q<Button>("SearchButton");
            connexion = root.Q<Button>("ConnectButton");
            panier = root.Q<Button>("Panier");

            username = root.Q<TextField>("UserName");
            password = root.Q<TextField>("Password");
            logInButton = root.Q<Button>("LogIn");

            List<Button> buttons = new List<Button> { home, profil, search, connexion, panier };
            foreach (var button in buttons)
            {
                button.clickable.clicked += () => OnButtonTouch(button);
            }

            logInButton.clickable.clicked += OnLogInButtonClicked;
        }
        private void OnLogInButtonClicked()
        {
            string usernameValue = username.value;
            string passwordValue = password.value;

            if (string.IsNullOrEmpty(usernameValue) || string.IsNullOrEmpty(passwordValue))
            {
                Debug.LogWarning("Username or password is empty");
                return;
            }

            // Call the SignUp method from LogManager
            StartCoroutine(LogManager.Instance.LogIn(usernameValue, passwordValue));
            UIManager.Instance.ChangeScreen(UIManager.Instance.currentScreen, UIManager.Instance.profil);
        }

        void OnButtonTouch(Button button)
        {
            UIManager.Instance.OnButtonTouch(button);
        }
    }
}
