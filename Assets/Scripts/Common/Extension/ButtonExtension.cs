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

public static class ToggleExtension
{
    public static void SetClickListener(this Toggle toggle, Action<bool> onClick)
    {
        toggle.onValueChanged.RemoveAllListeners();
        toggle.onValueChanged.AddListener((isOn) => onClick(isOn));
    }
}