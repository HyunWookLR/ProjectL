using UnityEngine;
using UnityEngine.UI;
public struct LoginLoadData : ILoadSceneData
{
    public SceneType PreviousScene { get => SceneType.Login; }
    public User User { get; private set; }
    public LoginLoadData(User user)
    {
        User = user;
    }
}
public class LoginController : MonoBehaviour
{
    [SerializeField] private Button googleLoginButton = null;
    [SerializeField] private Button guestLoginButton = null;
    [SerializeField] private Button startButton = null;
    [SerializeField] private Button logoutButton = null;
    [SerializeField] private GameObject loginButtonsParent = null;
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
            SceneLoader.Instance.Load(SceneType.Main, new LoginLoadData(User.Create()));
        });

        guestLoginButton.SetClickListener(async () =>
        {
            BlockCanvas.Instance.Activate();
            await BackEndLogin.GuestLogin();
            BlockCanvas.Instance.Deactivate();
            SetStartButton(true);
        });
        logoutButton.SetClickListener(async() =>
        {
            BlockCanvas.Instance.Activate();
            await BackEndLogout.Logout();
            BlockCanvas.Instance.Deactivate();
            SetStartButton(false);
        });
        //TODO apk����� ���۽����� ����ؾ� �׽�Ʈ ����
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

        loginButtonsParent.SetActive(!isActive);
    }

    //private void GoogleSignUp()
    //{
    //    PlayGamesClientConfiguration config = new PlayGamesClientConfiguration
    //    .Builder()
    //    .RequestServerAuthCode(false)
    //    .RequestEmail() // �̸��� ������ �䱸�ϱ� ���� �߰��ؾ� �մϴ�.
    //    .RequestIdToken()
    //    .Build();
    //}
}
