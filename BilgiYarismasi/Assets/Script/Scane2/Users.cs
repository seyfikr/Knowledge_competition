using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Users
{
    public string userName;
    public int level;
    public bool loginStatus;
    public Users(string userName,int level,bool loginStaatus)
    {
        this.userName = userName;
        this.level = level;
        this.loginStatus= loginStaatus;
    }
}
