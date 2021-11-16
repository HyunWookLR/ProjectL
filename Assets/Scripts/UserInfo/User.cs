using System.Collections;
using System.Collections.Generic;
using BackEnd;
using UnityEngine;

public class User
{
    public string InDate { get; private set; }
    public string NickName { get; private set; }


    public static User Create()
    {
        var user = new User();
        return user;
    }

    private User()
    {
        InDate = Backend.UserInDate;
        NickName = Backend.UserNickName;

        Debug.Log("Indate:" + InDate);
        Debug.Log("nickname:" + NickName);
    }
}
