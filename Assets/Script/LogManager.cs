using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace ARQuea
{
    public class LogManager : MonoBehaviour
    {
        public static LogManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public IEnumerator SignUp(string username, string password, string email)
        {
            WWWForm form = new WWWForm();

            form.AddField("username", username);
            form.AddField("password", password);
            form.AddField("email", email);

            UnityWebRequest www = UnityWebRequest.Post("http://localhost/TestPHP/register.php", form);
            yield return www.SendWebRequest();


            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Sign Up Error: " + www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log("Sign Up Response: " + responseText);
                // Vous pouvez traiter la réponse JSON ici
            }
        }

        public IEnumerator LogIn(string username, string password)
        {
            WWWForm form = new WWWForm();
            form.AddField("username", username);
            form.AddField("password", password);

            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/TestPHP/login.php", form))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Login Error: " + www.error);
                }
                else
                {
                    string responseText = www.downloadHandler.text;
                    Debug.Log("Login Response: " + responseText);
                    // Parse JSON response here and handle accordingly
                }
            }
        }
    }
}