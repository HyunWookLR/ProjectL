using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCanvas : MonoSingleton<LoadingCanvas>
{
    //TODO Loading���ڸ� �ϳ��� ���� �ִϸ��̼�

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
