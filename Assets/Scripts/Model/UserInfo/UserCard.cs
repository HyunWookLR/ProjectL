using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

public class AcquiredCard
{
    [JsonProperty("rows")] private List<AcquiredCardRow> rows = new List<AcquiredCardRow>();
    public class AcquiredCardRow
    {
        [JsonProperty("acquiredCard")] public AcquiredCardList UserCardList { get; private set; }
        public class AcquiredCardList
        {
            [JsonProperty("L")] public List<AllKey> Keys { get; private set; }
        }
    }

    public List<int> GetAcquiredCardIdList()
    {
        List<int> acquiredList = new List<int>();
        if(rows.HasItem())
        {
            var allKeys = rows.First().UserCardList.Keys;
            foreach (var key in allKeys)
            {
                var stringNumber = key.GetKey();
                int number;
                if(int.TryParse(stringNumber, out number))
                {
                    acquiredList.Add(number);
                }
            }
        }
        return acquiredList;
    }
}

