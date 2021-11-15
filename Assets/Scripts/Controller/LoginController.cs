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
        //TODO Scene넘어갈때 필요한 값 전달할 수 있는 SceneManager싱글톤만들기 저장했다가 코루틴으로 한프레임지나고 이벤트발산
        startButton.SetClickListener(()=> { });
        guestLoginButton.SetClickListener(async () =>
        {
            await BackEndLogin.GuestLogin();
            SetStartButton(true);
        });
        logoutButton.SetClickListener(Logout);
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
