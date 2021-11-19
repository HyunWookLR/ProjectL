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
        //TODO User�� �ִ� ������ ī���ϵ��� ��������

        //�̺�������� �ʿ��ϹǷ� Ŭ�� ��ü ����Ʈ �����ϱ�
    }
}
