using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanierManager : MonoBehaviour
{
    public Card card;
    public static PanierManager Instance;
    public List<ItemsSO> panierItems = new List<ItemsSO>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Assure que l'instance persiste entre les scènes
        }
        else
        {
            Destroy(gameObject); // Détruit les instances en double
        }
    }

    public void AddToPanier(ItemsSO item)
    {
        panierItems.Add(item);
    }
}