using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
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

    private List<CatalogCard> catalogCards = new List<CatalogCard>();
    private List<CatalogCard> objectPool = new List<CatalogCard>();
    public void Init(List<CardInfo> cards, bool hasAcheived)
    {
        catalogCards.Clear();
        for (int index = 0; index < cards.Count; index++)
        {
            var row = index / maxLength;
            var column = index % maxLength;

            var catalogCard = PopCard();
            var position = new Vector2(startWidthPadding + column * (widthPadding + catalogCard.Width), startHeightPadding + row * (heightPadding - catalogCard.Height));
            catalogCard.Init(position, cards[index], hasAcheived);
            catalogCards.Add(catalogCard);
        }
        InitSize(cards.Count);
    }

    public void SortReverse()
    {
        var reversePositions = new List<Vector2>();
        foreach (var card in catalogCards)
        {
            reversePositions.Add(card.Position);
        }

        catalogCards.Reverse();

        for (int index = 0; index < reversePositions.Count; index++)
        {
            catalogCards[index].RePosition(reversePositions[index]);
        }
    }

    private CatalogCard PopCard()
    {
        CatalogCard card = null;
        if(objectPool.Count <= 0)
        {
            card = Instantiate(Resources.Load<CatalogCard>("Prefab/Card/CatalogCard"), cardListRect, false);
        }
        else
        {
            card = objectPool.Last();
            card.gameObject.SetActive(true);
            objectPool.Remove(card);
        }
        return card;
    }

    private void PushCard(CatalogCard card)
    {
        card.gameObject.SetActive(false);
        objectPool.Add(card);
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
            var card = Resources.Load<CatalogCard>("Prefab/Card/CatalogCard");
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
