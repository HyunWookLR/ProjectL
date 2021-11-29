using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CatalogCardContainer : MonoBehaviour
{
    [SerializeField] private RectTransform TextRect = null;
    [SerializeField] private RectTransform cardListRect = null;

    private static readonly int maxLength = 4;
    private static readonly float startWidthPadding = 50f;
    private static readonly float startHeightPadding = -60f;
    private static readonly float widthPadding = 40f;
    private static readonly float heightPadding = -60f;

    public void Init(List<CardInfo> cards)
    {
        for (int index = 0; index < cards.Count; index++)
        {
            var row = index / maxLength;
            var column = index % maxLength;

            var catalogCard = Instantiate(Resources.Load<CatalogCard>("Prefab/CatalogCard"), cardListRect, false);
            var position = new Vector2(startWidthPadding + column * (widthPadding + catalogCard.Width), startHeightPadding + row * (heightPadding - catalogCard.Height));
            catalogCard.Init(position);
        }
    }
}
