using BackEnd;
using UnityEngine;
using UnityEditor;

public class LoginAccountManagerEditor : EditorWindow
{
    [MenuItem("LoginAccount/DeleteLocalGuestAccount")]
    public static void ShowWindow()
    {
        //BackEndSDK가 작동중이어야 하므로 플레이 후 실행한다
        Backend.BMember.DeleteGuestInfo();
        Debug.Log("<color=green>GeustAccount Deleted!</color>");
    }
}