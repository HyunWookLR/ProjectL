using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCanvas : MonoSingleton<LoadingCanvas>
{
    //TODO Loading글자를 하나씩 띄우는 애니메이션

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
