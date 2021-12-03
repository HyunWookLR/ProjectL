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
        //카드 필터팝업창에 조건들을 입력하면 cardCtrl에 이벤트 전달.
        //cardCtrl가 catalogCardContainer에게 조건이 적용된 cardinfo 전달
    }
}
