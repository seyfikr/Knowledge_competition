using Firebase;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;



public class Login : MonoBehaviour
{
    FirebaseAuth auth;
    public Button loginButton;
    public Button registerButton;
    public Button resetPasswordButton;
    public TMP_InputField emailField;
    public TMP_InputField passwordField;
    public Image LoginPanel, UserNamePanel;
    public TMP_Text RegisterText;

    void Start()
    {
        //UserNamePanel.gameObject.SetActive(false);
        auth = FirebaseAuth.DefaultInstance;
        loginButton.onClick.AddListener(NewLogin);
        registerButton.onClick.AddListener(RegisterUser);
        resetPasswordButton.onClick.AddListener(ResetPassword);
    }
    public void NewLogin()
    {
        StartCoroutine(LoginCoroutine());
        //SignIn();
    }
  
    IEnumerator LoginCoroutine()
    {
        string email = emailField.text;
        string password = passwordField.text;
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => loginTask.IsCompleted);

        if (loginTask.IsCanceled)
        {
            RegisterText.text = "there is no such record";
            Debug.LogError("login canceled");
            yield break;
        }

        if (loginTask.IsFaulted)
        {
            RegisterText.text = "there is no such record";
            Debug.LogError("login encountered an error" + loginTask.Exception);
            yield break;
        }
        //Baþarýlý
        AuthResult result = loginTask.Result;
        FirebaseUser user = result.User;
        Debug.Log("logged in as: " + user.Email);
        LoginButon();


    }
    public void LoginButon()
    {       
        Debug.Log("girdi");      
        LoginPanel.gameObject.SetActive(false);
        UserNamePanel.gameObject.SetActive(true);      
    }
    public void RegisterUser()
    {
  
        StartCoroutine(RegisterCoroutine());
    }
    IEnumerator RegisterCoroutine()
    {
        string email = emailField.text;
        string password = passwordField.text;
        var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => registerTask.IsCompleted);

        if (registerTask.IsCanceled)
        {
            Debug.LogError("Registration canceled");
            RegisterText.text = "failed to register";
            yield break;
        
        }

        if (registerTask.IsFaulted)
        {
            Debug.LogError("Registration encountered an error: " + registerTask.Exception);
            RegisterText.text = "failed to register";
            yield break;
          
        }



        AuthResult result = registerTask.Result;
        FirebaseUser user = result.User;
        Debug.Log("Registered and logged in as: " + user.Email);
        RegisterText.text = "registration is successful";
    }

    public void ResetPassword()
    {
        string email = emailField.text;
        auth.SendPasswordResetEmailAsync(email).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("Password reset canceled");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("Password reset encountered an error: " + task.Exception);
                return;
            }

            Debug.Log("Password reset email sent successfully.");
        });
    }
    //2.SignIn Ýþlemi Örnegi;

    //void SignIn()
    //{
    //    string email = emailField.text;
    //    string password = passwordField.text;
    //    auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
    //    {
    //        if (task.IsCanceled)
    //        {

    //            Debug.LogError("login canceled");
    //            return;
    //        }

    //        if (task.IsFaulted)
    //        {
    //            RegisterText.text = "there is no such record";
    //            Debug.LogError("login encountered an error" );
    //            return;
    //        }

    //        AuthResult result = task.Result;
    //        FirebaseUser user = result.User;
    //    });
    //}

}
