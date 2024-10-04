using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using ARQuea;
using UnityEngine.Networking;

public class ARQueaTests
{
    private LogManager logManager;

    [SetUp]
    public void Setup()
    {
        GameObject logManagerObject = new GameObject();
        logManager = logManagerObject.AddComponent<LogManager>();
        LogManager.Instance = logManager;
    }

    [TearDown]
    public void TearDown()
    {
        if (logManager != null)
        {
            Object.Destroy(logManager.gameObject);
        }
    }

    [UnityTest]
    [Order(1)]
    public IEnumerator TestSignUp_Successful()
    {
        string username = "John05";
        string password = "Homlander1";
        string email = "johnSmith@aol.com";

        // S'attendre à un log contenant un succès dans la réponse JSON
        LogAssert.Expect(LogType.Log, new System.Text.RegularExpressions.Regex("Sign Up Response:.*\"success\":true.*"));


        yield return logManager.StartCoroutine(logManager.SignUp(username, password, email));

        LogAssert.NoUnexpectedReceived();
    }

    [UnityTest]
    [Order(2)]
    public IEnumerator TestSignUp_Failure()
    {
        UnityWebRequest.ClearCookieCache();

        string username = "John05";
        string password = "Homlander1";
        string email = "johnSmith@aol.com";

        // S'attendre à un log contenant une erreur dans la réponse JSON
        LogAssert.Expect(LogType.Log, new System.Text.RegularExpressions.Regex("Sign Up Response:.*\"success\":false.*"));

        yield return logManager.StartCoroutine(logManager.SignUp(username, password, email));

        LogAssert.NoUnexpectedReceived();
    }

    [UnityTest]
    [Order(3)]
    public IEnumerator TestLogIn_Successful()
    {
        string username = "John05";
        string password = "Homlander1";

        // S'attendre à ce que le log contienne un message de succès dans la structure JSON
        LogAssert.Expect(LogType.Log, new System.Text.RegularExpressions.Regex("Login Response:.*\"success\":true.*"));


        yield return logManager.StartCoroutine(logManager.LogIn(username, password));

        LogAssert.NoUnexpectedReceived();
    }

    [UnityTest]
    [Order(4)]
    public IEnumerator TestLogIn_Failure()
    {
        UnityWebRequest.ClearCookieCache();

        string username = "InvalidUser";
        string password = "WrongPassword";

        // S'attendre à un log contenant une erreur dans la réponse JSON
        LogAssert.Expect(LogType.Log, new System.Text.RegularExpressions.Regex("Login Response:.*\"success\":false.*"));

        yield return logManager.StartCoroutine(logManager.LogIn(username, password));

        LogAssert.NoUnexpectedReceived();
    }
}