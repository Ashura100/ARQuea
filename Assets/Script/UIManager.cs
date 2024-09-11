using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace ARQuea
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [SerializeField] ItemsRef itemsRef;

        [SerializeField] public GameObject kitchen;
        [SerializeField] public GameObject room;
        [SerializeField] public GameObject chamber;
        [SerializeField] public GameObject bathroom;

        [SerializeField] public GameObject home;
        [SerializeField] public GameObject search;
        [SerializeField] public GameObject itemsUI;
        [SerializeField] public GameObject log;
        [SerializeField] public GameObject signUp;
        [SerializeField] public GameObject login;
        [SerializeField] public GameObject panier;
        

        public GameObject currentScreen;

        private void Awake()
        {
            if (Instance == null)
            {
                // Recherche de l'instance existante dans la scène
                Instance = this;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            currentScreen = home;
        }

        public void OnButtonTouch(Button button)
        {
            switch (button.name)
            {
                case "Profil":
                    ChangeScreen(currentScreen, log);
                    break;
                case "SignUp":
                    ChangeScreen(currentScreen, signUp);
                    break;
                case "Login":
                    ChangeScreen(currentScreen, login);
                    break;
                case "Kitchen":
                    ChangeScreen(currentScreen, kitchen);
                    break;
                case "Room":
                    ChangeScreen(currentScreen, room);
                    break;
                case "Chamber":
                    ChangeScreen(currentScreen, chamber);
                    break;
                case "Bathroom":
                    ChangeScreen(currentScreen, bathroom);
                    break;
                case "HomeButton":
                    ChangeScreen(currentScreen, home);
                    break;
                case "SearchButton":
                    ChangeScreen(currentScreen, search);
                    break;
                case "ConnectButton":
                    ChangeScreen(currentScreen, log);
                    break;
                case "Panier":
                    ChangeScreen(currentScreen, panier);
                    break;
            }
        }

        public void ChangeScreen(GameObject fromScreen, GameObject toScreen, float speed = 1, ItemsSO items = null)
        {
            // Déplacer l'écran actuel vers la gauche
            fromScreen.transform.DOMoveX(-Screen.width, speed).OnComplete(() => {
                fromScreen.SetActive(false);

                // Préparer le nouvel écran en le plaçant à droite (hors de la vue)
                toScreen.SetActive(true);
                toScreen.transform.position = new Vector3(Screen.width, toScreen.transform.position.y, toScreen.transform.position.z);

                // Amener le nouvel écran vers le centre
                toScreen.transform.DOMoveX(0, speed);
                currentScreen = toScreen;

                itemsRef.DisplayData(items);  
            });
        }

        public void ComeBackFromAr()
        {
            itemsUI.SetActive(true);
            itemsRef.DisplayData(Items.Instance.GetSelectedItem());
        }
        
    }

}