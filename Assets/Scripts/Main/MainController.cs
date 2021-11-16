using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour, ISceneLoadListener
{
    private void Awake()
    {
        SceneLoader.Instance.AddListener(this);
    }

    public void OnHandleSceneEvent(ILoadSceneData sceneData)
    {
        if (sceneData is LoginLoadData loginLoadData)
        {
            Debug.Log("Get login Data");
        }
    }

    void Start()
    {

    }
}
