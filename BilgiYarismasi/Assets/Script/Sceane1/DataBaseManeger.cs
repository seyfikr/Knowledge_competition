using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using TMPro;
using Firebase.Auth;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Google.MiniJSON;

public class DataBaseManeger : MonoBehaviour
{
    public QuestManager questManager;
    private int Scor;
    private string userID;
    public TMP_InputField UserName;
    public TMP_InputField HighScore;
    private DatabaseReference dbReference;
    public Text HighScoreText;
    public Text ScorText;
    public GameObject succesPanel, wrongPanel;
    void Start()
    {
        Scor = 0;
        userID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        GetUserInfo();
    }
    public void CreateUser()
    {

        string userName = UserName.text;
        int highScore = 0;

        User newUser = new User(userName, highScore);
        string json = JsonUtility.ToJson(newUser);
        SceneManager.LoadScene(1);

        // Veri tabanýný gönderme
        //Child Tabloda Satýr açmaya yarýyor
        dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to create user: " + task.Exception);
                return;
            }

            Debug.Log("User created successfully!");
        });
    }
  
    private IEnumerator UpdateScoreCoroutine()
    {
        
      var getScoreTask = dbReference.Child("users").Child(userID).Child("highScore").GetValueAsync();
        yield return new WaitUntil(() => getScoreTask.IsCompleted);

        if (getScoreTask.IsFaulted)
        {
            Debug.LogError("Failed to get user high score: " + getScoreTask.Exception);
            yield break;
        }

        DataSnapshot snapshot = getScoreTask.Result;
        int currentHighScore = int.Parse(snapshot.Value.ToString());

         Scor += 100;
        ScorText.text = Scor.ToString();


        if (Scor >= currentHighScore)
        {
            User newUser = new User(UserName.text, Scor);
            string json = JsonUtility.ToJson(newUser);

            //veri tabaný Push
            var setScoreTask = dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json);
            yield return new WaitUntil(() => setScoreTask.IsCompleted);
            GetUserInfo();

            if (setScoreTask.IsFaulted)
            {
                Debug.LogError("Failed to set user high score: " + setScoreTask.Exception);
                yield break;
            }

            Debug.Log("User high score updated successfully!");
        }
        else
        {
            Debug.Log("New high score is not greater than or equal to current high score. No update needed.");
        }
    }
    public void falseQuiz()
    {
        Scor = 0;
        ScorText.text = Scor.ToString();
        questManager.OpenRandomPanel();
        StartCoroutine(WrongPanel());


    }

    public void UpdateScore()
    {


        StartCoroutine(UpdateScoreCoroutine());
        questManager.OpenRandomPanel();
        StartCoroutine(SuccesPanel());

    }
    public IEnumerator GetName(Action<string> onCallback)
    {
        var userNamedata = dbReference.Child("users").Child(userID).Child("name").GetValueAsync();
        yield return new WaitUntil(() => userNamedata.IsCompleted);

        if (userNamedata.IsFaulted)
        {
            Debug.LogError("Failed to get user name: " + userNamedata.Exception);
            yield break; 
        }

        DataSnapshot snapshot = userNamedata.Result;
        onCallback.Invoke(snapshot.Value.ToString());
    }

    public IEnumerator GetHighScore(Action<int> onCallback)
    {
        var userhighScoredata = dbReference.Child("users").Child(userID).Child("highScore").GetValueAsync();
        yield return new WaitUntil(() => userhighScoredata.IsCompleted);

        if (userhighScoredata.IsFaulted)
        {
            Debug.LogError("Failed to get user high score: " + userhighScoredata.Exception);
            yield break; 
        }

        DataSnapshot snapshot = userhighScoredata.Result;
        onCallback.Invoke(int.Parse(snapshot.Value.ToString()));
    }

    public void GetUserInfo()
    {
        //StartCoroutine(GetName((string name) =>
        //{
        //    NameText.text = "Name: " + name;
        //    Debug.Log("Name: " + name);
        //}));

        StartCoroutine(GetHighScore((int highscore) =>
        {
            HighScoreText.text = "High Score: " + highscore.ToString();
            Debug.Log("High Score: " + highscore);
        }));
    }
    IEnumerator WrongPanel()
    {
        wrongPanel.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        wrongPanel.SetActive(false);
    }
    IEnumerator SuccesPanel()
    {
        succesPanel.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        succesPanel.SetActive(false);
    }
}
