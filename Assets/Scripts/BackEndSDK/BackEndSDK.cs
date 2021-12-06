using System.Threading.Tasks;
using System.Text.RegularExpressions;
using UnityEngine;
using BackEnd;
public class BackEndSDK : MonoSingleton<BackEndSDK>
{
    private async void Awake()
    {
        var result = await InitAsync();
        if(result == true)
        {
            Debug.Log("<color=green>BackEnd Init Success</color>");
        }
        else
        {
            Debug.LogError("<color=red>BackEnd Init fail</color>");
        }
    }

    private void Update()
    {
        Backend.AsyncPoll();
    }
    
    public static void NotifyIfError(BackendReturnObject returnObject)
    {
        var statusCode = returnObject.GetStatusCode();
        if (Regex.IsMatch(statusCode, "401|403|400|503|408|429"))
        {
            throw new InvalidBackEndException(returnObject);
        }
    }

    public void ActiveOnScene() { Instance.enabled = true; }

    private async Task<bool> InitAsync()
    {
        var taskCompletionSource = new TaskCompletionSource<bool>();
        Backend.InitializeAsync(true, (callback) =>
        {
            taskCompletionSource.SetResult(callback.IsSuccess());
        });
        return await taskCompletionSource.Task;
    }
}
