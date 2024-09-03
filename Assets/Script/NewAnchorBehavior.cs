using UnityEngine;
using Vuforia;
using Lean.Touch;

namespace ARQuea
{
    public class NewAnchorBehavior : VuforiaMonoBehaviour
    {
        AnchorBehaviour anchorStage;
        PlaneFinderBehaviour planeFinder;

        LeanDragTranslate leanDragTranslate;

        [SerializeField] GameObject canvas;
        GameObject instantiatedObject;
        bool isObjectPlaced = false;
        bool isObjectFixed = false;

        private void Start()
        {
            planeFinder = GetComponent<PlaneFinderBehaviour>();
            planeFinder.HitTestMode = HitTestMode.AUTOMATIC;

            canvas = GetComponent<Canvas>().gameObject;

            if (canvas != null)
            {
                canvas.SetActive(false);  // Masquer le Canvas au démarrage
            }
        }

        private void Update()
        {
            if (instantiatedObject != null && !isObjectPlaced)
            {
                if (!isObjectFixed)
                {
                    // Si des entrées tactiles sont disponibles
                    if (LeanTouch.Fingers.Count == 3)
                    {
                        ProcessRotate(LeanTouch.Fingers[0]);
                    }

                    ProcessTranslate(LeanTouch.Fingers[0]);
                }

                // Basculer l'état de fixation en appuyant sur "Espace"
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isObjectFixed = !isObjectFixed; // Basculer l'état fixé/défixé

                    if (isObjectFixed)
                    {
                        // Rendre l'objet opaque une fois fixé
                        SetObjectTransparency(instantiatedObject, 1.0f);
                        Debug.Log("Object fixed at: " + instantiatedObject.transform.position);
                    }
                    else
                    {
                        // Rendre l'objet transparent une fois défixé
                        SetObjectTransparency(instantiatedObject, 0.5f);
                        Debug.Log("Object unfixed and can be moved.");
                    }
                }
            }

            // Vérifier si l'objet est fixé et touché
            if (isObjectFixed && instantiatedObject != null && LeanTouch.Fingers.Count > 0)
            {
                LeanFinger finger = LeanTouch.Fingers[0];
                Ray ray = Camera.main.ScreenPointToRay(finger.ScreenPosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == instantiatedObject.transform)
                    {
                        ShowCanvas();
                    }
                }
            }
        }

        public void HackPosition(HitTestResult hit)
        {
            if (instantiatedObject != null)
            {
                Debug.Log("An object is already instantiated.");
                return;
            }

            // Récupérer l'item sélectionné
            ItemsSO selectedItem = Items.Instance.GetSelectedItem();

            if (anchorStage == null)
            {
                // Création de l'AnchorBehaviour
                anchorStage = VuforiaBehaviour.Instance.ObserverFactory.CreateAnchorBehaviour("Configured Plane", hit);
            }
            

            if (anchorStage != null && anchorStage.name == "Configured Plane")
            {
                // Instancier l'objet sélectionné
                instantiatedObject = Instantiate(selectedItem.gameOject, Vector3.zero, Quaternion.identity, anchorStage.transform);

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

        void ProcessTranslate(LeanFinger finger)
        {
            instantiatedObject.GetComponent<LeanDragTranslate>().Camera = Camera.main;
        }

        void ShowCanvas()
        {
            if (canvas != null)
            {
                canvas.SetActive(true);
                Debug.Log("Canvas displayed.");
            }
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
