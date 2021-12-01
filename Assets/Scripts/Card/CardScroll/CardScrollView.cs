using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class CardScrollView : MonoBehaviour
{
    [SerializeField] private RectTransform contentRect = null;
    [SerializeField] private CatalogCardContainer acheivedContainer = null;
    [SerializeField] private CatalogCardContainer unAcheivedContainer = null;

    public void Init(User user)
    {
        acheivedContainer.Init(user.AcquiredCards, true);
        var notAcquiredCards = user.AllCards.Where((card) => user.AcquiredCards.NotContains(card)).ToList();
        unAcheivedContainer.Init(notAcquiredCards, false);

        var acheivedRect = acheivedContainer.GetComponent<RectTransform>();
        var unacheivedRect = unAcheivedContainer.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(acheivedRect);
        LayoutRebuilder.ForceRebuildLayoutImmediate(unacheivedRect);

        var contentLayoutGroup = contentRect.GetComponent<VerticalLayoutGroup>();
        float layoutGroupPadding = 0f;
        if(contentLayoutGroup != null)
        {
            layoutGroupPadding = contentLayoutGroup.padding.top + contentLayoutGroup.padding.bottom + contentLayoutGroup.spacing;
        }
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, acheivedRect.sizeDelta.y + unacheivedRect.sizeDelta.y + layoutGroupPadding);
    }
}
