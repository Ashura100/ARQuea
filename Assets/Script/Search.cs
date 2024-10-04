using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using System.Linq;

namespace ARQuea
{
    public class Search : MonoBehaviour
    {
        public static Search Instance;
        [SerializeField] public UIDocument uIDocument;
        private VisualElement root;

        public const string CATEGORIE = "http://localhost/TestPHP/projet5.php?CatId=";
        public const string url = "http://localhost/TestPHP/projet5.php?lettre=";

        TextField searchText;
        Button searchEnter;
        Button kitchen;
        Button room;
        Button chamber;
        Button bathroom;

        Button profil;
        Button home;
        Button connexion;
        Button panier;

        public List<ItemsSO> items = new List<ItemsSO>();

        private void Awake()
        {
            Instance = this;  // Assurez-vous que l'instance est définie dans Awake
        }

        private void OnEnable()
        {
            root = uIDocument.rootVisualElement;

            searchEnter = root.Q<Button>("SearchEnter");
            profil = root.Q<Button>("Profil");
            searchText = root.Q<TextField>("Search");
            kitchen = root.Q<Button>("Kitchen");
            room = root.Q<Button>("Room");
            chamber = root.Q<Button>("Chamber");
            bathroom = root.Q<Button>("Bathroom");

            home = root.Q<Button>("HomeButton");
            connexion = root.Q<Button>("ConnectButton");
            panier = root.Q<Button>("Panier");

            // Associe chaque bouton de catégorie à son ID
            Dictionary<Button, string> categoryButtons = new Dictionary<Button, string>
        {
            { bathroom, "1" },  // Par exemple, "1" pour Kitchen
            { chamber, "2" },     // "2" pour Room
            { kitchen, "3" },  // "3" pour Chamber
            { room, "4" }  // "4" pour Bathroom
        };

            // Utiliser une boucle pour assigner le callback avec l'ID de chaque catégorie
            foreach (var buttonPair in categoryButtons)
            {
                var categoryButton = buttonPair.Key;
                var categoryId = buttonPair.Value;

                // Utilisation de la closure pour passer l'ID de la catégorie au moment de l'appel
                categoryButton.clickable.clicked += () => OnButtonCategTouch(categoryButton, categoryId);
            }

            List<Button> buttons = new List<Button> { profil, home, connexion, panier };
            foreach (var button in buttons)
            {
                button.clickable.clicked += () => OnButtonTouch(button);
            }

            searchEnter.clickable.clicked += () => OnButtonEnterClick(searchEnter);
        }

        // Modification ici pour utiliser la valeur du champ de recherche et effectuer le changement d'écran
        void OnButtonEnterClick(Button button)
        {
            // Utilise la valeur actuelle du champ de recherche
            string searchValue = searchText.value;

            // Vérifiez que la recherche a au moins 3 lettres
            if (searchValue.Length < 3)
            {
                Debug.Log("Veuillez fournir au moins 3 lettres pour la recherche.");
                return;
            }

            // Démarre la coroutine pour la recherche
            StartCoroutine(SearchArticles(searchValue));
        }

        // Coroutine pour effectuer la recherche d'articles
        IEnumerator SearchArticles(string searchValue)
        {
            string searchUrl = url + searchValue; // Créer l'URL pour la recherche
            using (UnityWebRequest webRequest = UnityWebRequest.Get(searchUrl))
            {
                // Envoyer la requête et attendre la réponse
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError(webRequest.error);
                }
                else
                {
                    // Traiter la réponse JSON
                    string jsonResponse = webRequest.downloadHandler.text;
                    ProcessSearchResults(jsonResponse);
                }
            }
        }

        // Traitement des résultats de la recherche
        void ProcessSearchResults(string jsonData)
        {
            if (string.IsNullOrEmpty(jsonData))
            {
                Debug.Log("Aucun article trouvé.");
                return;
            }

            // Désérialiser les données JSON en objets ItemsSO
            JArray jArray = JArray.Parse(jsonData);
            items.Clear();

            if (jArray.Count > 0)
            {
                foreach (JObject itemData in jArray.Children<JObject>())
                {
                    ItemsSO newItem = new ItemsSO
                    {
                        name = itemData.GetValue("Nom").ToString(),
                        price = float.Parse(itemData.GetValue("Prix").ToString()),
                        size = new Vector3(
                            float.Parse(itemData.GetValue("TailZ").ToString()),
                            float.Parse(itemData.GetValue("TailY").ToString()),
                            float.Parse(itemData.GetValue("TailX").ToString())
                        ),
                        image = Resources.Load<Sprite>(itemData.GetValue("Image").ToString()),
                        gameOject = Resources.Load<GameObject>(itemData.GetValue("Model").ToString())
                    };

                    // Ajoutez l'article à la liste
                    items.Add(newItem);
                    Debug.Log("Article trouvé : " + newItem.name);
                }

                // Changer d'écran pour afficher les détails de l'article
                UIManager.Instance.ChangeScreen(UIManager.Instance.currentScreen, UIManager.Instance.itemsUI, 0.5f, items[0]);
            }
            else
            {
                Debug.Log("Aucun article trouvé correspondant à votre recherche.");
            }
        }

        void OnButtonCategTouch(Button categoryButton, string categoryId)
        {
            UIManager.Instance.OnButtonTouch(categoryButton);
            StartCoroutine(GetCategoryData(categoryId));
        }

        void OnButtonTouch(Button button)
        {
            UIManager.Instance.OnButtonTouch(button);
        }

        // Coroutine pour effectuer la requête web avec l'ID de la catégorie
        IEnumerator GetCategoryData(string categoryId)
        {
            string url = CATEGORIE + categoryId;
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError(webRequest.error);
                }
                else
                {
                    // Show results as text
                    Debug.Log(webRequest.downloadHandler.text);

                    // Process the data as needed
                    ProcessCategoryData(webRequest.downloadHandler.text);
                }
            }
        }

        //parse la categorie
        void ProcessCategoryData(string jsonData)
        {
            JArray jArray = JArray.Parse(jsonData);
            JObject jo = jArray.Children<JObject>().FirstOrDefault();

            Debug.Log(jo.GetValue("NomCat"));

            items.Clear();

            foreach (JObject itemData in jArray.Children<JObject>())
            {
                // Créer une nouvelle instance d'ItemsSO pour chaque item du JSON
                ItemsSO newItem = new ItemsSO();

                // Supposons que le JSON contient des champs comme "Nom", "Prix", etc.
                // On attribue ces valeurs à l'instance d'ItemsSO
                newItem.name = itemData.GetValue("Nom").ToString(); // Assurez-vous que le nom du champ JSON correspond
                newItem.price = float.Parse(itemData.GetValue("Prix").ToString());
                newItem.size = new Vector3(
                        float.Parse(itemData.GetValue("TailZ").ToString()),
                        float.Parse(itemData.GetValue("TailY").ToString()),
                        float.Parse(itemData.GetValue("TailX").ToString())
                    );
                newItem.image = Resources.Load<Sprite>(itemData.GetValue("Image").ToString());
                GameObject go = Resources.Load<GameObject>(itemData.GetValue("Model").ToString());
                newItem.gameOject = go;

                // Ajoutez l'item à la liste des items
                items.Add(newItem);

                // Debug log pour vérifier les éléments ajoutés
                Debug.Log("Added item: " + newItem.name);
            }

            // Après avoir ajouté tous les items, vous pouvez éventuellement déclencher une mise à jour de l'interface utilisateur
            UpdateUIWithItems();
        }

        // Méthode pour mettre à jour l'interface utilisateur avec les items
        void UpdateUIWithItems()
        {
            // Implémentez la logique pour afficher les items sur l'interface utilisateur
            // Par exemple, vous pouvez itérer sur la liste des items et les afficher dans un conteneur
            foreach (ItemsSO item in items)
            {
                // Ajoutez le code ici pour instancier des éléments d'interface utilisateur et les remplir avec les données de l'item
                Debug.Log("Displaying item: " + item.name);
            }
        }
    }
}