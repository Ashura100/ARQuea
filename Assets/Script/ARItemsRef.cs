using System.Collections;
using System.Collections.Generic;
using ARQuea;
using Lean.Touch;
using UnityEngine;
using UnityEngine.UI;

public class ARItemsRef : MonoBehaviour
{
    [SerializeField] Image sprite;      // R�f�rence � l'�l�ment Image pour afficher l'image de l'item
    [SerializeField] Text objectName;   // R�f�rence � l'�l�ment Text pour afficher le nom de l'item
    [SerializeField] Text price;        // R�f�rence � l'�l�ment Text pour afficher le prix de l'item
    [SerializeField] Text size;         // R�f�rence � l'�l�ment Text pour afficher la taille de l'item

    private Quaternion originalRotation; // Rotation originale du canvas pour qu'il suive la cam�ra
    public bool isShowing = false;      // Indique si l'objet AR est actuellement visible
    public bool isPlaced = true;        // Indique si l'objet est plac� dans la sc�ne AR

    private ItemsSO currentItem;        // Stocke l'item actuellement s�lectionn�

    // Start est appel� avant la premi�re frame
    void Start()
    {
        originalRotation = transform.rotation;  // Sauvegarde de la rotation originale
        PrefabManager.Instance.Hide();          // Cache le prefab AR au d�marrage

        // R�cup�re l'item s�lectionn� depuis le script Items
        currentItem = Items.Instance.GetSelectedItem();

        // S'il y a un item s�lectionn�, on met � jour le canvas
        if (currentItem != null)
        {
            SetupData(currentItem);
        }
        else
        {
            Debug.LogWarning("No item selected from Items.");
        }
    }

    // Update est appel� � chaque frame
    void Update()
    {
        // Ajuster la rotation du canvas pour qu'il suive toujours la cam�ra
        transform.rotation = Camera.main.transform.rotation * originalRotation;
    }

    // Cette m�thode configure et affiche les donn�es de l'item s�lectionn�
    public void SetupData(ItemsSO items)
    {
        currentItem = items;  // Stocke l'item en cours

        // Met � jour les composants UI du canvas avec les informations de l'item
        sprite.sprite = items.image;
        objectName.text = items.name;
        price.text = items.price.ToString() + "�";
        size.text = $"{items.size.x}m x {items.size.y}m x {items.size.z}m";
    }

    // G�re les interactions tactiles pour afficher ou masquer l'objet AR
    public void HandleFingerTap(LeanFinger finger)
    {
        if (isShowing)
        {
            PrefabManager.Instance.Hide();  // Cache l'objet AR si d�j� affich�
            isShowing = false;
        }
        else if (isPlaced)
        {
            PrefabManager.Instance.Show();  // Affiche l'objet AR si plac� et non affich�
            isShowing = true;
        }
    }

    // M�thode pour mettre � jour l'item actuel � partir d'une requ�te web ou autre source
    public void UpdateCurrentItem(ItemsSO newItem)
    {
        currentItem = newItem;

        // Mettre � jour le canvas avec les nouvelles informations
        SetupData(currentItem);
    }
}