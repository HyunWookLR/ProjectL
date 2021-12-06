using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldController : MonoBehaviour, ISceneLoadListener, IEventListener
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
            //cardScrollView.Init(userData.User);
        }
    }

    public void OnHandleEvent(IEventParam param)
    {
        
    }
}
