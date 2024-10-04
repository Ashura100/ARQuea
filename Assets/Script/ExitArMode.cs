using UnityEngine;
using UnityEngine.SceneManagement;

namespace ARQuea
{
    public class ExitArMode : MonoBehaviour
    {
        public void ChangeScene()
        {
            UIManager.Instance.ComeBackFromAr();
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("SampleScene"));
            SceneManager.UnloadSceneAsync("ARScene");
        }
    }
}
