using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour, ISceneLoadListener
{
    [SerializeField] private Button worldButton = null;
    [SerializeField] private Button cardButton = null;
    [SerializeField] private Button gachaButton = null;
    [SerializeField] private Button settingButton = null;

    private User user = null;
    private void Awake()
    {
        SceneLoader.Instance.AddListener(this);
        cardButton.SetClickListener(() => StartCoroutine(SceneLoader.Instance.LoadAsync(SceneType.Card, new SceneLoadUserData (user, SceneType.Main))));
        worldButton.SetClickListener(()=> StartCoroutine(SceneLoader.Instance.LoadAsync(SceneType.World, new SceneLoadUserData(user, SceneType.Main))));
        //TODO 기능구현
        settingButton.SetClickListener(()=> Debug.Log("기능 미구현"));
        gachaButton.SetClickListener(()=> Debug.Log("기능 미구현"));
    }

    public void OnHandleSceneEvent(ILoadSceneData sceneData)
    {
        if (sceneData is SceneLoadUserData userData)
        {
            user = userData.User;
        }
    }

    void Start()
    {

    }


}
