using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class DBManager2 : MonoBehaviour
{
    private DatabaseReference usersReference;

   
    void Start()
    {
        usersReference = FirebaseDatabase.DefaultInstance.RootReference;
        Initialization();
    }

    
    void Initialization()
    {

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => 
        {
        var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                //i�lemler
                Debug.Log("Ba�land�");
                usersReference = FirebaseDatabase.DefaultInstance.GetReference("users");
                SaveData("seyfi",50,true);
            }
            else
            {
                UnityEngine.Debug.Log(System.String.Format("Hata{0}", dependencyStatus));
            }
        
        });
    }
    void SaveData(string userName,int level,bool loginStatus)
    {
        Debug.Log("e");
        Users user=new Users(userName,level,loginStatus);
        string json=JsonUtility.ToJson(user);
        string userID = usersReference.Push().Key;
        usersReference.Child(userID).SetRawJsonValueAsync(json);
    }
}
