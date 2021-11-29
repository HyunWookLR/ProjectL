using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CardController : MonoBehaviour, ISceneLoadListener
{
    [SerializeField] private NavigationBar navigation = null;
    [SerializeField] private CardScrollView cardScrollView = null;
    private void Awake()
    {
        SceneLoader.Instance.AddListener(this);
    }

    public void OnHandleSceneEvent(ILoadSceneData sceneData)
    {
        if (sceneData is SceneLoadUserData userData)
        {
            navigation.Init(userData.User, SceneType.Main);
            cardScrollView.Init(userData.User);
        }
    }
}
