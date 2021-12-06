using System.Collections.Generic;
using BackEnd;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

public class User
{
    public string InDate { get; private set; }
    public string NickName { get; private set; }

    public List<CardInfo> AcquiredCards { get; private set; }
    public List<CardInfo> AllCards { get; private set; }
    public static async Task<User> CreateAsync()
    {
        var cards =await BackEndJsonDeserializer<CardInfo>.DeserializeChartAsync("allCards");
        var startCards = await BackEndJsonDeserializer<CardInfo>.DeserializeChartAsync("startCards");
        var acquiredCards = await BackEndJsonDeserializer<AcquiredCard>.DeserializeMyDataAsync("UserCard");
        return new User(cards, startCards, acquiredCards);
    }

    private User(List<CardInfo> cards, List<CardInfo> startCards, AcquiredCard acquiredCards)
    {
        InDate = Backend.UserInDate;
        NickName = Backend.UserNickName;
        AllCards = cards;
        InitAcquiredCard(startCards, acquiredCards);

        Debug.Log("Indate(UID):" + InDate);
    }

    private void InitAcquiredCard(List<CardInfo> startCards, AcquiredCard acquiredKeys)
    {
        AcquiredCards = new List<CardInfo>();
        //스타트팩 목록
        foreach (var card in startCards)
        {
            if (AcquiredCards.Where((containCard)=>containCard.Id == card.Id).NotHaveItem())
            {
                var cardInfo = AllCards.Find((item) => item.Id == card.Id);
                if(cardInfo != null)
                {
                    AcquiredCards.Add(cardInfo);
                }
            }
        }

        //사용자가 진행하며 습득한 목록
        var idList = acquiredKeys.GetAcquiredCardIdList();
        foreach (var id in idList)
        {
            if (AcquiredCards.Where((containCard) => containCard.Id == id).NotHaveItem())
            {
                var cardInfo = AllCards.Find((item) => item.Id == id);
                if (cardInfo != null)
                {
                    AcquiredCards.Add(cardInfo);
                }
            }
        }
    }
}
