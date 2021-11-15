using UnityEngine;
using UnityEngine.UI;


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
        //TODO Scene�Ѿ�� �ʿ��� �� ������ �� �ִ� SceneManager�̱��游��� �����ߴٰ� �ڷ�ƾ���� �������������� �̺�Ʈ�߻�
        startButton.SetClickListener(()=> { });
        guestLoginButton.SetClickListener(async () =>
        {
            await BackEndLogin.GuestLogin();
            SetStartButton(true);
        });
        logoutButton.SetClickListener(Logout);
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

    private async void Logout()
    {
        await BackEndLogout.Logout();
        SetStartButton(false);
    }

    private async void Login()
    {
        await BackEndLogin.GuestLogin();
    }
}
