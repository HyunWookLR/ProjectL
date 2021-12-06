using UnityEngine;
using DG.Tweening;
using TMPro;
public class LoginStartButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text = null;
    void Start()
    {
        text.DOFade(0f, 0f);

        DOTween.Sequence()
            .Append(text.DOFade(0.8f, 1f))
            .Append(text.DOFade(0f, 1f))
            .SetLoops(-1);
    }

    private void OnDisable()
    {
        text.DOKill();
    }

    private void OnDestroy()
    {
        text.DOKill();
    }
}
