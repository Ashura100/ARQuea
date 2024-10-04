using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ARQuea
{
    public class SignUp : MonoBehaviour
    {
        [SerializeField] public UIDocument uIDocument;
        private VisualElement root;
        private Button home;
        private Button profil;
        private Button search;
        private Button connexion;
        private Button panier;
        private TextField username;
        private TextField mailAddress;
        private TextField password;
        private TextField confirmPassword;
        private Button signUpButton;

        private void OnEnable()
        {
            root = uIDocument.rootVisualElement;
            home = root.Q<Button>("HomeButton");
            profil = root.Q<Button>("Profil");
            search = root.Q<Button>("SearchButton");
            connexion = root.Q<Button>("ConnectButton");
            panier = root.Q<Button>("Panier");

            username = root.Q<TextField>("UserName");
            mailAddress = root.Q<TextField>("Mail");
            password = root.Q<TextField>("Password");
            confirmPassword = root.Q<TextField>("ConfirmPassword");
            signUpButton = root.Q<Button>("SignUp");

            List<Button> buttons = new List<Button> { home, profil, search, connexion, panier };
            foreach (var button in buttons)
            {
                button.clickable.clicked += () => OnButtonTouch(button);
            }

            signUpButton.clickable.clicked += OnSignUpButtonClicked;
        }

        private void OnSignUpButtonClicked()
        {
            string usernameValue = username.value;
            string passwordValue = password.value;
            string confirmPasswordValue = confirmPassword.value;
            string mailAddressValue = mailAddress.value;

            if (string.IsNullOrEmpty(usernameValue) || string.IsNullOrEmpty(passwordValue) ||
                string.IsNullOrEmpty(confirmPasswordValue) || string.IsNullOrEmpty(mailAddressValue))
            {
                Debug.LogWarning("All fields must be filled");
                return;
            }

            if (passwordValue != confirmPasswordValue)
            {
                Debug.LogWarning("Passwords do not match");
                return;
            }

            // Call the SignUp method from LogManager
            StartCoroutine(LogManager.Instance.SignUp(usernameValue, passwordValue, mailAddressValue));
            UIManager.Instance.ChangeScreen(UIManager.Instance.currentScreen, UIManager.Instance.login);
        }

        void OnButtonTouch(Button button)
        {
            UIManager.Instance.OnButtonTouch(button);
        }
    }

}