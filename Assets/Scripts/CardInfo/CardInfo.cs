using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class CardInfo
{
    [JsonProperty("Id")] public int Id { get; private set; }
    [JsonProperty("Hp")] public int Hp { get; private set; }
    [JsonProperty("Atk")] public int Atk { get; private set; }
    [JsonProperty("CardName")] public string CardName { get; private set; }

#if UNITY_EDITOR
    public void SetId(int id) => Id = id;
    public void SetHp(int hp) => Hp = hp;
    public void SetAtk(int atk) => Atk = atk;
    public void SetCardName(string name) => CardName = name;
#endif

}
