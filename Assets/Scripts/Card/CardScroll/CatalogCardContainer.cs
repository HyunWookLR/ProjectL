using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CatalogCardContainer : MonoBehaviour
{
    [SerializeField] private RectTransform TextRect = null;
    [SerializeField] private RectTransform cardListRect = null;
    [SerializeField] private VerticalLayoutGroup layoutGroup = null;

    private static readonly int maxLength = 4;
    private static readonly float startWidthPadding = 50f;
    private static readonly float startHeightPadding = -60f;
    private static readonly float widthPadding = 40f;
    private static readonly float heightPadding = -60f;

    public void Init(List<CardInfo> cards, bool hasAcheived)
    {
        for (int index = 0; index < cards.Count; index++)
        {
            var row = index / maxLength;
            var column = index % maxLength;

            var catalogCard = Instantiate(Resources.Load<CatalogCard>("Prefab/CatalogCard"), cardListRect, false);
            var position = new Vector2(startWidthPadding + column * (widthPadding + catalogCard.Width), startHeightPadding + row * (heightPadding - catalogCard.Height));
            catalogCard.Init(position, cards[index], hasAcheived);
        }

        InitSize(cards.Count);
    }

    private void InitSize(int cardsCount)
    {
        var rectHeight = 0f;
        rectHeight += TextRect.sizeDelta.y;
        if (layoutGroup != null)
        {
            rectHeight += layoutGroup.padding.top + layoutGroup.padding.bottom + layoutGroup.spacing;
        }
        var cardListHeight = 0f;
        if (cardsCount > 0)
        {
            var row = cardsCount / maxLength + 1;
            var card = Resources.Load<CatalogCard>("Prefab/CatalogCard");
            cardListHeight += row * card.Height;
            cardListHeight += (row - 1) * Mathf.Abs(heightPadding);
            cardListHeight += Mathf.Abs(startHeightPadding * 2f);
        }
        else
        {
            rectHeight = 1f;
        }
        cardListRect.sizeDelta = new Vector2(cardListRect.sizeDelta.x, cardListHeight);

        rectHeight += cardListHeight;

        var rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, rectHeight);
    }
}
