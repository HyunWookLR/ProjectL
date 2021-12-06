using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = Instantiate(Resources.Load<T>("Prefab/Singleton/" + typeof(T).Name));
            }
            DontDestroyOnLoad(instance);
            return instance;
        }
    }
}
