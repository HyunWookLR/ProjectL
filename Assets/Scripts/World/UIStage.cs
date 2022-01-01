using System.Collections.Generic;
using UnityEngine;

public class UIStage : MonoBehaviour, IEventListener
{
    [SerializeField] private Transform cellRoot = null;
    [SerializeField] private StageCell cellPrefab = null;

    private List<StageCell> stages = new List<StageCell>();
    private readonly int totalStage = 7;
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    public void Init(int worldNumber, Type worldType)
    {
        gameObject.SetActive(true);
        InitStageCells(worldNumber, worldType);
    }

    private void InitStageCells(int worldNumber, Type worldType)
    {
        InitExistList();
        for (int index = 1; index <= totalStage; index++)
        {
            StageCell item = null;
            if (index >= stages.Count)
            {
                item = CreateCell();
                stages.Add(item);
            }
            else
            {
                item = stages[index];
                item.gameObject.SetActive(true);
            }
            item.Init(worldType, worldNumber, index);
        }
    }
    private void InitExistList()
    {
        stages.Clear();
        var items = cellRoot.GetComponentsInChildren<StageCell>();
        foreach (var item in items)
        {
            item.gameObject.SetActive(false);
            stages.Add(item);
        }
    }
    private StageCell CreateCell()
    {
        return Instantiate(cellPrefab, cellRoot, false);
    }

    public void OnHandleEvent(IEventParam param)
    {
        if(param is StageClickEvent clickEvent)
        {
            //TODO 배틀씬 진입하기
        }
    }
}
