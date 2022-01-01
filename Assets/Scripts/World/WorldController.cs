using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour, ISceneLoadListener, IEventListener
{
    [SerializeField] private NavigationBar navigation = null;
    [SerializeField] private GameObject uiWorld = null;
    [SerializeField] private Transform cellRoot = null;
    [SerializeField] private WorldCell cellPrefab = null;
    [SerializeField] private UIStage uiStage = null;

    private List<WorldCell> worlds = new List<WorldCell>();

    private void Awake()
    {
        SceneLoader.Instance.AddListener(this);
    }
    private async void InitWorldCellsAsync()
    {
        InitExistList();
        var worldInfo = await BackEndJsonDeserializer<WorldInfo>.DeserializeChartAsync("worlds");
        for (int index = 0; index < worldInfo.Count; index++)
        {
            WorldCell item = null;
            if (index >= worlds.Count)
            {
                item = CreateCell();
                worlds.Add(item);
            }
            else
            {
                item = worlds[index];
                item.gameObject.SetActive(true);
            }
            item.Init(worldInfo[index]);
        }
    }
    private void InitExistList()
    {
        worlds.Clear();
        var items = cellRoot.GetComponentsInChildren<WorldCell>();
        foreach (var item in items)
        {
            item.gameObject.SetActive(false);
            worlds.Add(item);
        }
    }
    private WorldCell CreateCell()
    {
        return Instantiate(cellPrefab, cellRoot, false);
    }

    public void OnHandleSceneEvent(ILoadSceneData sceneData)
    {
        if (sceneData is SceneLoadUserData userData)
        {
            navigation.Init(userData.User, SceneType.Main);
            InitWorldCellsAsync();
        }
    }

    public void OnHandleEvent(IEventParam param)
    {
        if(param is WorldClickEvent clickEvent)
        {
            uiWorld.SetActive(false);
            uiStage.Init(clickEvent.WorldNumber, clickEvent.WorldType);
        }
    }
}
