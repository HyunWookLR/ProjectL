using UnityEngine;
using UnityEngine.UI;

public struct SortReverseEvent : IEventParam
{ }
public class SortButton : MonoBehaviour
{
    [SerializeField] private Button sortButton = null;
    [SerializeField] private Image sortImage = null;
    [SerializeField] private Sprite[] sprites = new Sprite[imageSize];

    private static readonly int imageSize = 2;
    void Awake()
    {
        sortImage.sprite = sprites[0];
        sortButton.SetClickListener(ChangeSort);
    }

    private void ChangeSort()
    {
        sortImage.sprite = sortImage.sprite == sprites[1] ? sprites[0] : sprites[1];
        this.Emit(new SortReverseEvent());
    }
}
