using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class CardScrollView : MonoBehaviour
{
    [SerializeField] private RectTransform contentRect = null;
    [SerializeField] private CatalogCardContainer acheivedContainer = null;
    [SerializeField] private CatalogCardContainer unAcheivedContainer = null;

    //TODO: Container1,2 ī�� ����ְ� forcelayout ��, height�����ͼ� contentRect.RectTransform���
    public void Init(User user)
    {
        acheivedContainer.Init(user.AcquiredCards);
        var notAcquiredCards = user.AllCards.Where((card) => user.AcquiredCards.NotContains(card)).ToList();
        unAcheivedContainer.Init(notAcquiredCards);
    }
}
