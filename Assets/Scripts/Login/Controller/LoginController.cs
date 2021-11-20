using UnityEngine;
using UnityEngine.UI;
public struct SceneLoadUserData : ILoadSceneData
{
    public SceneType PreviousScene { get; private set; }
    public User User { get; private set; }
    public SceneLoadUserData(User user, SceneType previousScene)
    {
        User = user;
        PreviousScene = previousScene;
    }
}
public class LoginController : MonoBehaviour
{
    [SerializeField] private Button googleLoginButton = null;
    [SerializeField] private Button guestLoginButton = null;
    [SerializeField] private Button startButton = null;
    [SerializeField] private Button logoutButton = null;
    [SerializeField] private GameObject loginButtonsParent = null;
    [SerializeField] private Button deleteAccountButton = null;
    private void Awake()
    {
        BackEndSDK.Instance.ActiveOnScene();
        SetButtons();
        if (BackEndLogin.TryAutoLogin())
        {
            SetStartButton(true);
        }
        else
        {
            SetStartButton(false);
        }
    }

    private void SetButtons()
    {
        startButton.SetClickListener(()=> 
        {
            SceneLoader.Instance.Load(SceneType.Main, new SceneLoadUserData(User.Create(), SceneType.Login));
        });
        deleteAccountButton.SetClickListener(() => DeleteAccount());
        guestLoginButton.SetClickListener(() => GuestLogIn());
        logoutButton.SetClickListener(() => LogOut());

        //TODO delete
        googleLoginButton.SetClickListener(() => Debug.Log("기능 미구현"));

        //TODO apk만들고 구글스토어에서 등록해야 테스트 가능
        //googleLoginButton.SetClickListener(async ()=>
        //{
        //    await BackEndSignUp.OnClickSignUp();
        //    SetStartButton(true);
        //});
    }

    private void SetStartButton(bool isActive)
    {
        startButton.gameObject.SetActive(isActive);
        logoutButton.gameObject.SetActive(isActive);
        deleteAccountButton.gameObject.SetActive(isActive);

        loginButtonsParent.SetActive(!isActive);
    }

    //private void GoogleSignUp()
    //{
    //    PlayGamesClientConfiguration config = new PlayGamesClientConfiguration
    //    .Builder()
    //    .RequestServerAuthCode(false)
    //    .RequestEmail() // 이메일 권한을 요구하기 위해 추가해야 합니다.
    //    .RequestIdToken()
    //    .Build();
    //}
    private void DeleteAccount()
    {
        if (BackEndDeleteAccount.TryDeleteAccount())
        {
            Debug.Log("Deleted local&server Account");
            LogOut();
        }
    }

    private async void GuestLogIn()
    {
        BlockCanvas.Instance.Activate();
        await BackEndLogin.GuestLogin();
        BlockCanvas.Instance.Deactivate();
        SetStartButton(true);
    }

    private async void LogOut()
    {
        BlockCanvas.Instance.Activate();
        await BackEndLogout.Logout();
        BlockCanvas.Instance.Deactivate();
        SetStartButton(false);
    }
}
