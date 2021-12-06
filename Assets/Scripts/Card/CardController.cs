using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CardController : MonoBehaviour, ISceneLoadListener, IEventListener
{
    [SerializeField] private NavigationBar navigation = null;
    [SerializeField] private CardScrollView cardScrollView = null;
    [SerializeField] private Button filterButton = null;
    private void Awake()
    {
        SceneLoader.Instance.AddListener(this);
        filterButton.SetClickListener(() =>
        {
            var filterPopup = Instantiate(Resources.Load<FilterPopup>("Prefab/Card/Popup/FilterPopup"), transform);
            filterPopup.transform.SetAsLastSibling();
            navigation.transform.SetAsLastSibling();
        });
    }

    public void OnHandleSceneEvent(ILoadSceneData sceneData)
    {
        if (sceneData is SceneLoadUserData userData)
        {
            navigation.Init(userData.User, SceneType.Main);
            cardScrollView.Init(userData.User);
        }
    }

    public void OnHandleEvent(IEventParam param)
    {
        if(param is SortReverseEvent)
        {
            cardScrollView.SortReverse();
        }


        //TODO
        //ī�� �����˾�â�� ���ǵ��� �Է��ϸ� cardCtrl�� �̺�Ʈ ����.
        //cardCtrl�� catalogCardContainer���� ������ ����� cardinfo ����
    }
}
