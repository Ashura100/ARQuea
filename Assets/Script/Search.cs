using ARQuea;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using System.Linq;

public class Search : MonoBehaviour
{
    public static Search Instance;
    [SerializeField] public UIDocument uIDocument;
    private VisualElement root;

    public const string CATEGORIE = "http://localhost/TestPHP/projet5.php?CatId=";

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
        Instance = this;  // Assurez-vous que l'instance est d�finie dans Awake
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

        // Associe chaque bouton de cat�gorie � son ID
        Dictionary<Button, string> categoryButtons = new Dictionary<Button, string>
        {
            { bathroom, "1" },  // Par exemple, "1" pour Kitchen
            { chamber, "2" },     // "2" pour Room
            { kitchen, "3" },  // "3" pour Chamber
            { room, "4" }  // "4" pour Bathroom
        };

        // Utiliser une boucle pour assigner le callback avec l'ID de chaque cat�gorie
        foreach (var buttonPair in categoryButtons)
        {
            var categoryButton = buttonPair.Key;
            var categoryId = buttonPair.Value;

            // Utilisation de la closure pour passer l'ID de la cat�gorie au moment de l'appel
            categoryButton.clickable.clicked += () => OnButtonCategTouch(categoryButton, categoryId);
        }

        List<Button> buttons = new List<Button> { profil, home, connexion, panier };
        foreach (var button in buttons)
        {
            button.clickable.clicked += () => OnButtonTouch(button);
        }

        //searchText.RegisterValueChangedCallback(evt => OnSearchTextChanged(evt.newValue));

        //searchEnter.clickable.clicked += () => OnButtonEnterClick(searchEnter);
    }

    /*private void OnSearchTextChanged(string newValue)
    {
        SearchItems(newValue);
    }

    // Fonction de recherche d'items avec le texte fourni
    public List<ItemsSO> SearchItems(string searchTextValue)
    {
        // Cr�er une liste pour stocker les r�sultats de recherche
        List<ItemsSO> foundItems = new List<ItemsSO>();

        // It�rer sur tous les items dans la liste 'items' (d�j� remplie depuis la requ�te web)
        foreach (ItemsSO item in items)
        {
            // Comparer le texte de recherche et le nom de l'item en mode insensible � la casse
            if (item.name.IndexOf(searchTextValue, System.StringComparison.OrdinalIgnoreCase) >= 0)
            {
                foundItems.Add(item);
            }
        }

        return foundItems;
    }

    // Modification ici pour utiliser la valeur du champ de recherche et effectuer le changement d'�cran
    void OnButtonEnterClick(Button button)
    {
        // Utilise la valeur actuelle du champ de recherche
        string searchValue = searchText.value;

        // Appelle SearchItems pour obtenir les r�sultats de recherche
        List<ItemsSO> foundItems = SearchItems(searchValue);

        // Affiche les items si trouv�s, sinon affiche un message d'erreur
        if (foundItems.Count > 0)
        {
            foreach (ItemsSO foundItem in foundItems)
            {
                Debug.Log("Found item: " + foundItem.name);

                // On r�cup�re le prefab du foundItem et on l'affiche dans itemsUI
                //GameObject itemPrefab = foundItem.gameObject; // Assurez-vous que le prefab est bien r�f�renc�

                // Appelle ChangeScreen pour afficher itemsUI, vous pouvez d�cider d'afficher plusieurs items
                UIManager.Instance.ChangeScreen(UIManager.Instance.currentScreen, UIManager.Instance.itemsUI, 0.5f, foundItem);
            }
        }
        else
        {
            Debug.Log("No items found matching: " + searchValue);
        }
    }*/


    void OnButtonCategTouch(Button categoryButton, string categoryId)
    {
        UIManager.Instance.OnButtonTouch(categoryButton);
        StartCoroutine(GetCategoryData(categoryId));
    }

    void OnButtonTouch(Button button)
    {
        UIManager.Instance.OnButtonTouch(button);
    }

    // Coroutine pour effectuer la requ�te web avec l'ID de la cat�gorie
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
            // Cr�er une nouvelle instance d'ItemsSO pour chaque item du JSON
            ItemsSO newItem = new ItemsSO();

            // Supposons que le JSON contient des champs comme "Nom", "Prix", etc.
            // On attribue ces valeurs � l'instance d'ItemsSO
            newItem.name = itemData.GetValue("Nom").ToString(); // Assurez-vous que le nom du champ JSON correspond
            newItem.price = float.Parse(itemData.GetValue("Prix").ToString());
            newItem.size = new Vector3(
                    float.Parse(itemData.GetValue("TailZ").ToString()),
                    float.Parse(itemData.GetValue("TailY").ToString()),
                    float.Parse(itemData.GetValue("TailX").ToString())
                );
            newItem.image = Resources.Load<Sprite>(itemData.GetValue("Image").ToString());
            //newItem.gameOject = Instantiate(Resources.Load(itemData.GetValue("Model"))) as GameObject;

            // Ajoutez l'item � la liste des items
            items.Add(newItem);

            // Debug log pour v�rifier les �l�ments ajout�s
            Debug.Log("Added item: " + newItem.name);
        }

        // Apr�s avoir ajout� tous les items, vous pouvez �ventuellement d�clencher une mise � jour de l'interface utilisateur
        UpdateUIWithItems();
    }

    // M�thode pour mettre � jour l'interface utilisateur avec les items
    void UpdateUIWithItems()
    {
        // Impl�mentez la logique pour afficher les items sur l'interface utilisateur
        // Par exemple, vous pouvez it�rer sur la liste des items et les afficher dans un conteneur
        foreach (ItemsSO item in items)
        {
            // Ajoutez le code ici pour instancier des �l�ments d'interface utilisateur et les remplir avec les donn�es de l'item
            Debug.Log("Displaying item: " + item.name);
        }
    }
}