using UnityEngine;
using TMPro;
using UnityEngine.UI;

public struct StageClickEvent : IEventParam
{
    public int WorldNumber { get; private set; }
    public int StageIndex { get; private set; }
    public Type WorldType { get; private set; }
    public StageClickEvent(int worldNumber, int stageIndex, Type type)
    {
        WorldNumber = worldNumber;
        StageIndex = stageIndex;
        WorldType = type;
    }
}
public class StageCell : MonoBehaviour
{
    [SerializeField] private TMP_Text stageName = null;
    [SerializeField] private Button cellButton = null;
    public void Init(Type worldType,int worldNumber, int stageIndex)
    {
        stageName.text = $"{worldNumber}-{stageIndex}";
        cellButton.SetClickListener(() =>
        {
            this.Emit(new StageClickEvent(worldNumber, stageIndex, worldType));
        });
    }
}
