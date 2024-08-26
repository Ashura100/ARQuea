using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ARQuea
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [SerializeField] public GameObject home;
        [SerializeField] public GameObject search;
        [SerializeField] public GameObject itemsUI;
        [SerializeField] public GameObject log;
        [SerializeField] public GameObject panier;
        

        public GameObject currentScreen;

        private void Awake()
        {
            if (Instance == null)
            {
                // Recherche de l'instance existante dans la sc�ne
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
                case "HomeButton":
                    ChangeScreen(currentScreen, home);
                    break;
                case "SearchButton":
                    ChangeScreen(currentScreen, search);
                    break;
                /*case "TouchToPlay":
                    ChangeScreen(currentScreen, gameUi);
                    break;*/
                case "ConnectButton":
                    ChangeScreen(currentScreen, log);
                    break;
                case "Panier":
                    ChangeScreen(currentScreen, panier);
                    break;
            }
        }

        public void ChangeScreen(GameObject fromScreen, GameObject toScreen, float speed = 0.5f)
        {
            // D�placer l'�cran actuel vers la gauche
            fromScreen.transform.DOMoveX(-Screen.width, speed).OnComplete(() => {
                fromScreen.SetActive(false);

                // Pr�parer le nouvel �cran en le pla�ant � droite (hors de la vue)
                toScreen.SetActive(true);
                toScreen.transform.position = new Vector3(Screen.width, toScreen.transform.position.y, toScreen.transform.position.z);

                // Amener le nouvel �cran vers le centre
                toScreen.transform.DOMoveX(0, speed);
                currentScreen = toScreen;
            });
        }
    }

}