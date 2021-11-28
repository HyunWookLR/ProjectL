using System.Collections.Generic;
using BackEnd;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

public class User
{
    public string InDate { get; private set; }
    public string NickName { get; private set; }

    private List<CardInfo> acquiredCards = new List<CardInfo>();
    private List<CardInfo> allCards = new List<CardInfo>();
    public static async Task<User> CreateAsync()
    {
        var cards =await BackEndJsonDeserializer<CardInfo>.DeserializeChart("allCards");
        var startCards = await BackEndJsonDeserializer<CardInfo>.DeserializeChart("startCards");
        var acquiredCards = await BackEndJsonDeserializer<AcquiredCard>.DeserializeMyData("UserCard");
        return new User(cards, startCards, acquiredCards);
    }

    private User(List<CardInfo> cards, List<CardInfo> startCards, AcquiredCard acquiredCards)
    {
        InDate = Backend.UserInDate;
        NickName = Backend.UserNickName;
        allCards = cards;
        InitAcquiredCard(startCards, acquiredCards);

        Debug.Log("Indate(UID):" + InDate);
    }

    private void InitAcquiredCard(List<CardInfo> startCards, AcquiredCard acquiredKeys)
    {
        //스타트팩 목록
        foreach (var card in startCards)
        {
            if (acquiredCards.Where((containCard)=>containCard.Id == card.Id).NotHaveItem())
            {
                var cardInfo = allCards.Find((item) => item.Id == card.Id);
                if(cardInfo != null)
                {
                    acquiredCards.Add(cardInfo);
                }
            }
        }

        //사용자가 진행하며 습득한 목록
        var idList = acquiredKeys.GetAcquiredCardIdList();
        foreach (var id in idList)
        {
            if (acquiredCards.Where((containCard) => containCard.Id == id).NotHaveItem())
            {
                var cardInfo = allCards.Find((item) => item.Id == id);
                if (cardInfo != null)
                {
                    acquiredCards.Add(cardInfo);
                }
            }
        }
    }
}
