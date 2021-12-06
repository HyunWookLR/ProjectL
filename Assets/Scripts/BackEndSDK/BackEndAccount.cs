using UnityEngine;
using BackEnd;
using System.Threading.Tasks;

public class BackEndSignUp
{
    //ȸ�������� �ϴ� ��� ȸ�����԰� ���ÿ� �α����� ����˴ϴ�.
    public static async Task<bool> SignUpAsync(string federationToken, FederationType type, string etc = "")
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
        return await taskCompletionSource.Task;
    }
}

public class BackEndLogin
{
    public static async Task<bool> GuestLoginAsync(string etc = "")
    {
        //TODO ȣ�� ��, �ۿ��� ���� ������ ����, ���� �缳ġ, ��� ����, ���� ������ �ջ� ���� ��Ȳ�� �߻��ϸ� �Խ�Ʈ ������ ������ �� ���ٰ� �ݵ�� �������� ����� ������ �����ؾ� �մϴ�.
        var taskCompletionSource = new TaskCompletionSource<bool>();
        Backend.BMember.GuestLogin(etc, (returnObject) =>
        {
            try
            {
                BackEndSDK.NotifyIfError(returnObject);
                taskCompletionSource.SetResult(returnObject.IsSuccess());
                Debug.Log("Guest Id: " + Backend.BMember.GetGuestID() + "\n Login Succeed");
            }
            catch (InvalidBackEndException e)
            {
                ErrorPopup.Instance.Show(e.Message, () => { });
            }
        });
        return await taskCompletionSource.Task;
    }

    //Guest, GoogleAccount �ڵ� �α���
    public static bool TryAutoLogin()
    {
        var returnObject = Backend.BMember.LoginWithTheBackendToken();
        return returnObject.IsSuccess();
    }

    //���� �α����ߴ� ��⿡�� �α���, ��ū ���� �� �ڵ� ����
    public static async Task<bool> TokenLoginAsync(string etc = "")
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
        return await taskCompletionSource.Task;
    }
}

public class BackEndLogout
{
    public static async Task<bool> LogoutAsync()
    {
        var taskCompletionSource = new TaskCompletionSource<bool>();
        BlockCanvas.Instance.Activate();
        Backend.BMember.Logout((resultObject) =>
        {
            taskCompletionSource.SetResult(true);
            Debug.Log("Log Out Success");
            BlockCanvas.Instance.Deactivate();
        });
        return await taskCompletionSource.Task;
    }
}

public class BackEndDeleteAccount
{
    //����,���� ���� ����
    public static bool TryDeleteAccount()
    {
        string id = Backend.BMember.GetGuestID();
        if(id.NotEmpty())
        {
            Debug.Log("������ ���̵� :" + id);
            Backend.BMember.DeleteGuestInfo();
            Backend.BMember.SignOut();
            return true;
        }
        else
        {
            return false;
        }
    }
}