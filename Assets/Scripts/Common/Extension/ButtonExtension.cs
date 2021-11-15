using System;
using UnityEngine.UI;
public static class ButtonExtension
{
    public static void SetClickListener(this Button button, Action onClick)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener( ()=> onClick());
    }
}
