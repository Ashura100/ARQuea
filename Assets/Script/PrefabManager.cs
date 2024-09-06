using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager Instance;

    [SerializeField] LeanDragTranslate leanDragTranslate;
    [SerializeField] ARItemsRef ar;

    private void Awake()
    {
        if (Instance == null)
        {
            // Recherche de l'instance existante dans la scène
            Instance = this;
        }
    }

    public void ActiveTranslate()
    {
        gameObject.GetComponent<LeanDragTranslate>().enabled = true;
        gameObject.GetComponent<LeanDragTranslate>().Camera = Camera.main;
    }

    public void DesactiveTranslate()
    {
        gameObject.GetComponent<LeanDragTranslate>().enabled = false;
        gameObject.GetComponent<LeanDragTranslate>().Camera = null;
    }

    public void Show()
    {
        ar.gameObject.SetActive(true);
    }

    public void Hide()
    {
        ar.gameObject.SetActive(false);
    }
}
