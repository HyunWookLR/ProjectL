using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface ILoadSceneData
{
    SceneType PreviousScene { get; }
};

public interface ISceneLoadListener
{
    void OnHandleSceneEvent(ILoadSceneData sceneData);
};

public class SceneLoader : MonoSingleton<SceneLoader>
{
    private ILoadSceneData loadSceneData = null;
    private List<ISceneLoadListener> listeners = new List<ISceneLoadListener>();

    public IEnumerator LoadAsync(SceneType scene, ILoadSceneData sceneData)
    {
        loadSceneData = sceneData;
        SceneManager.sceneLoaded += OnSceneLoaded;
        AsyncOperation aysncOperation = SceneManager.LoadSceneAsync(scene.ToString());

        LoadingCanvas.Instance.Activate();
        while (!aysncOperation.isDone)
        {
            yield return null;
        }
    }

    public void AddListener(ISceneLoadListener listener)
    {
        if (listeners.NotContains(listener))
        {
            listeners.Add(listener);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        StartCoroutine(SendLoadSceneData());
        SceneManager.sceneLoaded -= OnSceneLoaded;
        LoadingCanvas.Instance.Deactivate();
    }

    private IEnumerator SendLoadSceneData()
    {
        yield return new WaitForEndOfFrame();
        foreach (var listener in listeners)
        {
            listener.OnHandleSceneEvent(loadSceneData);
        }
        listeners.Clear();
    }
}
