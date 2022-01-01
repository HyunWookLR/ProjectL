using UnityEngine;
using Newtonsoft.Json;
using System;
public class CardInfo
{
    [JsonProperty("Cid")] private SKey cid;
    [JsonProperty("Name")] private SKey name;
    [JsonProperty("Rare")] private SKey rare;
    [JsonProperty("Type")] private SKey type;
    [JsonProperty("Hp")] private SKey hp;
    [JsonProperty("Atk")] private SKey atk;

    public int Id { get => int.Parse(cid.Key); }
    public string CardName { get => name.Key; }
    public Rareness Rareness
    {
        get
        {
            try
            {
                if (Enum.TryParse(rare.Key, true, out Rareness rareness))
                {
                    return rareness;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return Rareness.N;
            }
        }
    }
    public Type CardType
    {
        get
        {
            try
            {
                Type cardType;
                if (Enum.TryParse(type.Key, true, out cardType))
                {
                    return cardType;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return Type.Red;
            }
        }
    }
    public int CardHp { get => int.Parse(hp.Key); }
    public int CardAtk { get => int.Parse(atk.Key); }
}