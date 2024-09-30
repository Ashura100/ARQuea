using ARQuea;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ExitArMode : MonoBehaviour
{
    public void ChangeScene()
    {
        UIManager.Instance.ComeBackFromAr();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("SampleScene"));
        SceneManager.UnloadSceneAsync("ARScene");
    }
}
