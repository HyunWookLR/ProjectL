using UnityEngine;
using BackEnd;
using System.Threading.Tasks;

public class BackEndSignUp
{
    //회원가입을 하는 경우 회원가입과 동시에 로그인이 진행됩니다.
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
        //TODO 호출 전, 밖에서 로컬 데이터 삭제, 어플 재설치, 기기 변경, 로컬 데이터 손상 등의 상황이 발생하면 게스트 계정에 접근할 수 없다고 반드시 유저에게 충분한 설명을 고지해야 합니다.
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

    //Guest, GoogleAccount 자동 로그인
    public static bool TryAutoLogin()
    {
        var returnObject = Backend.BMember.LoginWithTheBackendToken();
        return returnObject.IsSuccess();
    }

    //기존 로그인했던 기기에서 로그인, 토큰 만료 시 자동 갱신
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
                        ErrorPopup.Instance.Show("차단된 유저입니다. 고객센터로 문의주세요.", () => { });
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
    //로컬,서버 계정 삭제
    public static bool TryDeleteAccount()
    {
        string id = Backend.BMember.GetGuestID();
        if(id.NotEmpty())
        {
            Debug.Log("삭제할 아이디 :" + id);
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