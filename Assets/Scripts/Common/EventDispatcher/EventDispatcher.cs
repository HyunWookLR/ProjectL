using UnityEngine;

public interface IEventParam { }
public interface IEventListener
{
    void OnHandleEvent(IEventParam param);
}

public static class EventDispatcher
{
    public static void Emit(this MonoBehaviour monoBehaviour, IEventParam param)
    {
        Invoke(monoBehaviour, param);
    }
    public static void Invoke(this MonoBehaviour monoBehaviour, IEventParam param)
    {
        var eventListeners = monoBehaviour.GetComponentsInParent<IEventListener>();
        foreach (var eventListener in eventListeners)
        {
            eventListener.OnHandleEvent(param);
        }
    }
}
