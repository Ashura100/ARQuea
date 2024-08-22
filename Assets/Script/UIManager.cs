using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARQuea
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [SerializeField] public GameObject itemsUI;

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
            //currentScreen = itemsUI;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ChangeScreen(GameObject fromScreen, GameObject toScreen, float speed = 0.5f)
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
            });
        }
    }

}