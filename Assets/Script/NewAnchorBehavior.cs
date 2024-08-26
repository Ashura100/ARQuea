using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Vuforia;
using Lean.Touch;

namespace ARQuea
{
    public class NewAnchorBehavior : VuforiaMonoBehaviour
    {
        AnchorBehaviour anchorStage;
        PlaneFinderBehaviour planeFinder;

        GameObject instantiatedObject;
        bool isObjectPlaced = false;

        private void Start()
        {
            planeFinder = GetComponent<PlaneFinderBehaviour>();
            planeFinder.HitTestMode = HitTestMode.AUTOMATIC;
        }

        private void Update()
        {
            if (instantiatedObject != null && !isObjectPlaced)
            {
                // Si des entrées tactiles sont disponibles
                if (LeanTouch.Fingers.Count == 1)
                {
                    ProcessRotate(LeanTouch.Fingers[0]);
                }

                // Fixer l'objet en appuyant sur "Espace"
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isObjectPlaced = true;

                    // Rendre l'objet opaque
                    SetObjectTransparency(instantiatedObject, 1.0f);

                    Debug.Log("Object placed and fixed at: " + instantiatedObject.transform.position);
                }
            }
        }

        public void HackPosition(HitTestResult hit)
        {
            // Récupérer l'item sélectionné
            ItemsSO selectedItem = Items.Instance.GetSelectedItem();

            // Création de l'AnchorBehaviour
            anchorStage = VuforiaBehaviour.Instance.ObserverFactory.CreateAnchorBehaviour("Configured Plane", hit);

            if (anchorStage != null && anchorStage.name == "Configured Plane")
            {
                // Instancier l'objet sélectionné
                instantiatedObject = Instantiate(selectedItem.gameOject, Vector3.zero, Quaternion.identity, anchorStage.transform);

                // Ajouter LeanTranslate pour permettre le déplacement
                instantiatedObject.AddComponent<LeanDragTranslate>();

                // Rendre l'objet transparent
                SetObjectTransparency(instantiatedObject, 0.5f);

                Debug.Log("Instantiated item: " + instantiatedObject.name);
            }
        }

        void ProcessRotate(LeanFinger finger)
        {
            // Rotation avec les gestes Lean Touch
            float rotationAmount = finger.ScaledDelta.x; // Utiliser le mouvement horizontal pour la rotation
            instantiatedObject.transform.Rotate(Vector3.up, rotationAmount);
        }

        void SetObjectTransparency(GameObject obj, float alpha)
        {
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                foreach (Material mat in renderer.materials)
                {
                    Color color = mat.color;
                    color.a = alpha;
                    mat.color = color;

                    // Assurez-vous que le mode de rendu est en mode transparent
                    mat.SetFloat("_Mode", 3);
                    mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    mat.SetInt("_ZWrite", 0);
                    mat.DisableKeyword("_ALPHATEST_ON");
                    mat.EnableKeyword("_ALPHABLEND_ON");
                    mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    mat.renderQueue = 3000;
                }
            }

        }
    }
}
