using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour, ISceneLoadListener
{
    [SerializeField] private NavigationBar navigation = null;

    private void Awake()
    {
        SceneLoader.Instance.AddListener(this);
    }

    public void OnHandleSceneEvent(ILoadSceneData sceneData)
    {
        if (sceneData is SceneLoadUserData userData)
        {
            navigation.Init(userData.User, SceneType.Main);
            Init(userData.User);
        }
    }

    private void Init(User user)
    {
        //TODO User에 있는 수집한 카드목록들을 가져오기

        //미보유목록이 필요하므로 클라에 전체 리스트 저장하기
    }
}
