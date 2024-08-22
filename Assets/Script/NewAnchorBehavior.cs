using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Vuforia;

namespace ARQuea
{
    public class NewAnchorBehavior : VuforiaMonoBehaviour
    {
        AnchorBehaviour anchorStage;

        public void HackPosition(HitTestResult hit)
        {
            // Récupérer l'item sélectionné
            ItemsSO selectedItem = Items.Instance.GetSelectedItem();

            // Création de l'AnchorBehaviour
            anchorStage = VuforiaBehaviour.Instance.ObserverFactory.CreateAnchorBehaviour("Configured Plane", hit);

            if (anchorStage != null && anchorStage.name == "Configured Plane")
            {
                // Instancier l'objet sélectionné
                GameObject go = Instantiate(selectedItem.gameOject, Vector3.zero, Quaternion.identity, anchorStage.transform);
                Debug.Log("Instantiated item: " + go.name);
            }
        }

    }
}