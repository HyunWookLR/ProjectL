using UnityEngine;
using Newtonsoft.Json;
using System;

public class WorldInfo
{
    [JsonProperty("TableId")] private SKey id;
    [JsonProperty("Type")] private SKey type;
    [JsonProperty("ImagePath")] private SKey imagePath;

    public int Id { get => int.Parse(id.Key); }
    public Type WorldType
    {
        get
        {
            try
            {
                if (Enum.TryParse(type.Key, true, out Type worldType))
                {
                    return worldType;
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
    public string Path { get => imagePath.Key; }
}
