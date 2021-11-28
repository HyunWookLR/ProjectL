using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ErrorPopup : MonoSingleton<ErrorPopup>
{
    [SerializeField] private TextMeshProUGUI content = null;
    [SerializeField] private Button confirmButton = null;
    [SerializeField] private TextMeshProUGUI buttonText = null;
    [SerializeField] private Transform boxTransform = null;

    public void Show(string str, Action action)
    {
        DOTween.Sequence()
            .Append(boxTransform.DOScale(0.8f, 0f))
            .Append(boxTransform.DOScale(1f, 0.3f).SetEase(Ease.InCubic));

        gameObject.SetActive(true);
        SetContent(str);
        AddButton("È®ÀÎ", action);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void SetContent(string str)
    {
        content.gameObject.SetActive(true);
        content.text = str;
    }

    private void AddButton(string str, Action action)
    {
        buttonText.text = str;
        confirmButton.SetClickListener( ()=>
        {
            if(action != null) action();
            Hide();
        });
    }

    private void OnDestroy()
    {
        boxTransform.DOKill();
    }
}
