using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class NavigationBar : MonoBehaviour
{
    [SerializeField] private Button home = null;
    public void Init(User user, SceneType destScene)
    {
        home.SetClickListener(() =>
        {
            SceneType sceneType;
            if( Enum.TryParse(SceneManager.GetActiveScene().name, out sceneType))
            {
                if(destScene == sceneType)
                {
                    ErrorPopup.Instance.Show($"You tried to load same scene: {sceneType.ToString()}", () => { });
                }
                else
                {
                    StartCoroutine(SceneLoader.Instance.LoadAsync(destScene, new SceneLoadUserData(user, sceneType)));
                }
            }
            else
            {
                ErrorPopup.Instance.Show($"{SceneManager.GetActiveScene().name}, not assigned in Enum", ()=> { });
            }
        });
    }
}
