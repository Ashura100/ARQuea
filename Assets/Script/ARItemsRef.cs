using System.Collections;
using System.Collections.Generic;
using ARQuea;
using Lean.Touch;
using UnityEngine;
using UnityEngine.UI;

public class ARItemsRef : MonoBehaviour
{
    [SerializeField] Image sprite;      // Référence à l'élément Image pour afficher l'image de l'item
    [SerializeField] Text objectName;   // Référence à l'élément Text pour afficher le nom de l'item
    [SerializeField] Text price;        // Référence à l'élément Text pour afficher le prix de l'item
    [SerializeField] Text size;         // Référence à l'élément Text pour afficher la taille de l'item

    private Quaternion originalRotation; // Rotation originale du canvas pour qu'il suive la caméra
    public bool isShowing = false;      // Indique si l'objet AR est actuellement visible
    public bool isPlaced = true;        // Indique si l'objet est placé dans la scène AR

    private ItemsSO currentItem;        // Stocke l'item actuellement sélectionné

    // Start est appelé avant la première frame
    void Start()
    {
        originalRotation = transform.rotation;  // Sauvegarde de la rotation originale
        PrefabManager.Instance.Hide();          // Cache le prefab AR au démarrage

        // Récupère l'item sélectionné depuis le script Items
        currentItem = Items.Instance.GetSelectedItem();

        // S'il y a un item sélectionné, on met à jour le canvas
        if (currentItem != null)
        {
            SetupData(currentItem);
        }
        else
        {
            Debug.LogWarning("No item selected from Items.");
        }
    }

    // Update est appelé à chaque frame
    void Update()
    {
        // Ajuster la rotation du canvas pour qu'il suive toujours la caméra
        transform.rotation = Camera.main.transform.rotation * originalRotation;
    }

    // Cette méthode configure et affiche les données de l'item sélectionné
    public void SetupData(ItemsSO items)
    {
        currentItem = items;  // Stocke l'item en cours

        // Met à jour les composants UI du canvas avec les informations de l'item
        sprite.sprite = items.image;
        objectName.text = items.name;
        price.text = items.price.ToString() + "€";
        size.text = $"{items.size.x}m x {items.size.y}m x {items.size.z}m";
    }

    // Gère les interactions tactiles pour afficher ou masquer l'objet AR
    public void HandleFingerTap(LeanFinger finger)
    {
        if (isShowing)
        {
            PrefabManager.Instance.Hide();  // Cache l'objet AR si déjà affiché
            isShowing = false;
        }
        else if (isPlaced)
        {
            PrefabManager.Instance.Show();  // Affiche l'objet AR si placé et non affiché
            isShowing = true;
        }
    }

    // Méthode pour mettre à jour l'item actuel à partir d'une requête web ou autre source
    public void UpdateCurrentItem(ItemsSO newItem)
    {
        currentItem = newItem;

        // Mettre à jour le canvas avec les nouvelles informations
        SetupData(currentItem);
    }
}