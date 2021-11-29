using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CatalogCard : MonoBehaviour
{
    [SerializeField] private Button button = null;
    [SerializeField] private RectTransform rectTransform = null;

    public float Width { get => rectTransform.rect.width; }
    public float Height { get => rectTransform.rect.height; }
    public void Init(Vector2 position)
    {
        button.SetClickListener(()=> { });

        rectTransform.anchoredPosition = position;
    }

}
