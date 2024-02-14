using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using TMPro;
using UnityEngine.UI;
public class Loginn : MonoBehaviour
{
    FirebaseAuth auth;
    public TMP_InputField LoginemailField;
    public TMP_InputField LoginpasswordField;
    public TMP_InputField SignInemailField;
    public TMP_InputField SignInpasswordField;
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }
    public void SignInn()
    {
        string email = LoginemailField.text;
        string password = LoginpasswordField.text;
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {

                Debug.LogError("login canceled");
                return;
            }

            if (task.IsFaulted)
            {

                Debug.LogError("login encountered an error");
                return;
            }

            AuthResult result = task.Result;
            FirebaseUser user = result.User;
        });
    }
    public void LogIn()
    {
        string email = SignInemailField.text;
        string password = SignInpasswordField.text;
        if (email != "" && password != "")
        {
            auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {

                    Debug.LogError("login canceled");
                    return;
                }

                if (task.IsFaulted)
                {

                    Debug.LogError("login encountered an error");
                    return;
                }

                AuthResult result = task.Result;
                FirebaseUser user = result.User;
                Debug.Log("BaþarýlýGiriþ");
            });
        }
    }
}
