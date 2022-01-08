using BackEnd;
using UnityEngine;
using UnityEditor;

public class LoginAccountManagerEditor : EditorWindow
{
    [MenuItem("LoginAccount/DeleteLocalGuestAccount")]
    public static void ShowWindow()
    {
        //BackEndSDK�� �۵����̾�� �ϹǷ� �÷��� �� �����Ѵ�
        Backend.BMember.DeleteGuestInfo();
        Debug.Log("<color=green>GeustAccount Deleted!</color>");
    }
}