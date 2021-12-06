using System.Collections.Generic;
using Newtonsoft.Json;
using BackEnd;
using System.Threading.Tasks;

public static class BackEndJsonDeserializer<T> where T : class
{
    private static readonly Dictionary<string, string> backEndTable;
    static BackEndJsonDeserializer()
    {
        backEndTable = new Dictionary<string, string>()
        {
            {"allCards", "33327" },
            {"startCards", "33330" }
        };
    }

    public static async Task<List<T>> DeserializeChartAsync(string tableKey)
    {
        var taskCompletionSource = new TaskCompletionSource<List<T>>();
        try
        {
            if (backEndTable.ContainsKey(tableKey))
            {
                Backend.Chart.GetChartContents(backEndTable[tableKey], (returnObject) =>
                {
                    if (returnObject.IsSuccess())
                    {
                        var someValue = returnObject.GetReturnValue();
                        taskCompletionSource.SetResult(JsonConvert.DeserializeObject<ServerJson<T>>(returnObject.GetReturnValue()).Rows);
                    }
                    else
                    {
                        throw new InvalidBackEndException(returnObject);
                    }
                });
            }
            else
            {
                throw new InvaliTableException(tableKey);
            }
        }
        catch (InvalidBackEndException e)
        {
            taskCompletionSource.SetResult(new List<T>());
            ErrorPopup.Instance.Show(e.Message, () => { });
        }
        return await taskCompletionSource.Task;
    }

    public static async Task<T> DeserializeMyDataAsync(string tableKey)
    {
        var taskCompletionSource = new TaskCompletionSource<T>();

        var result = Backend.GameData.GetMyData(tableKey, new Where());

        Backend.GameData.GetMyData(tableKey, new Where(), (returnObject) =>
        {
            try
            {
                if (returnObject.IsSuccess())
                {
                    var jsonData = returnObject.GetReturnValue();
                    taskCompletionSource.SetResult(JsonConvert.DeserializeObject<T>(jsonData));
                }
                else
                {
                    throw new InvalidBackEndException(returnObject);
                }
            }
            catch (InvalidBackEndException e)
            {
                taskCompletionSource.SetResult(null);
                ErrorPopup.Instance.Show(e.Message, () => { });
            }
        });

        return await taskCompletionSource.Task;
    }
}



public class ServerJson<T> where T : class
{
    [JsonProperty("rows")] private List<T> rows = new List<T>();

    public List<T> Rows => rows;
}

public class SKey
{
    [JsonProperty("S")] public string Key { get; private set; }
}

public class NKey
{
    [JsonProperty("N")] public string Key { get; private set; }
}

public class MKey
{
    [JsonProperty("M")] public string Key { get; private set; }
}

public class LKey
{
    [JsonProperty("L")] public string Key { get; private set; }
}

public class BoolKey
{
    [JsonProperty("BOOL")] public string Key { get; private set; }
}

public class AllKey
{
    [JsonProperty("S")] private string sKey;
    [JsonProperty("N")] public string nKey;
    [JsonProperty("M")] public string mKey;
    [JsonProperty("L")] public string lKey;
    [JsonProperty("BOOL")] public string bKey;

    public string GetKey()
    {
        if (sKey != null && sKey.NotEmpty()) return sKey;
        if (nKey != null && nKey.NotEmpty()) return nKey;
        if (mKey != null && mKey.NotEmpty()) return mKey;
        if (lKey != null && lKey.NotEmpty()) return lKey;
        if (bKey != null && bKey.NotEmpty()) return bKey;
        return "";
    }
}