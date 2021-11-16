using UnityEngine;
using BackEnd;
using System.Threading.Tasks;

public class BackEndSignUp
{
    //ȸ�������� �ϴ� ��� ȸ�����԰� ���ÿ� �α����� ����˴ϴ�.
    public static Task<bool> OnClickSignUp(string federationToken, FederationType type, string etc = "")
    {
        var taskCompletionSource = new TaskCompletionSource<bool>();
        Backend.BMember.AuthorizeFederation(federationToken, type, etc, (returnObject) =>
        {
            try
            {
                BackEndSDK.NotifyIfError(returnObject);
                taskCompletionSource.SetResult(returnObject.IsSuccess());
            }
            catch (InvalidBackEndException e)
            {
                ErrorPopup.Instance.Show(e.Message, ()=> { });
            }
        });
        return taskCompletionSource.Task;
    }
}

public class BackEndLogin
{
    public static Task<bool> GuestLogin(string etc = "")
    {
        //TODO ȣ�� ��, �ۿ��� ���� ������ ����, ���� �缳ġ, ��� ����, ���� ������ �ջ� ���� ��Ȳ�� �߻��ϸ� �Խ�Ʈ ������ ������ �� ���ٰ� �ݵ�� �������� ����� ������ �����ؾ� �մϴ�.
        var taskCompletionSource = new TaskCompletionSource<bool>();
        Backend.BMember.GuestLogin(etc, (returnObject) =>
        {
            try
            {
                BackEndSDK.NotifyIfError(returnObject);
                taskCompletionSource.SetResult(returnObject.IsSuccess());
                Debug.Log("Guest Login Success");
            }
            catch (InvalidBackEndException e)
            {
                ErrorPopup.Instance.Show(e.Message, () => { });
            }
        });
        return taskCompletionSource.Task;
    }

    public static bool TryAutoLogin()
    {
        var returnObject = Backend.BMember.LoginWithTheBackendToken();
        return returnObject.IsSuccess();
    }

    public static Task<bool> TokenLogin(string etc = "")
    {
        var taskCompletionSource = new TaskCompletionSource<bool>();

        var response = Backend.BMember.IsAccessTokenAlive();
        if(response.IsNotSuccess())
        {
            Debug.Log("Expired token -> Now refreshing token");
            Backend.BMember.RefreshTheBackendToken();
        }

        Backend.BMember.LoginWithTheBackendToken((returnObject) =>
        {
            try
            {
                BackEndSDK.NotifyIfError(returnObject);
                taskCompletionSource.SetResult(returnObject.IsSuccess());
            }
            catch (InvalidBackEndException e)
            {
                switch (returnObject.GetErrorCode())
                {
                    case "403":
                        ErrorPopup.Instance.Show("���ܵ� �����Դϴ�. �����ͷ� �����ּ���.", () => { });
                        break;
                    default:
                        ErrorPopup.Instance.Show(e.Message, () => { });
                        break;
                }
            }
        });
        return taskCompletionSource.Task;
    }
}

public class BackEndLogout
{
    public static Task<bool> Logout()
    {
        var taskCompletionSource = new TaskCompletionSource<bool>();
        BlockCanvas.Instance.Activate();
        Backend.BMember.Logout((resultObject) =>
        {
            taskCompletionSource.SetResult(true);
            Debug.Log("Log Out Success");
            BlockCanvas.Instance.Deactivate();
        });
        return taskCompletionSource.Task;
    }
}