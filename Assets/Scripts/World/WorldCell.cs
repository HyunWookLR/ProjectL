using UnityEngine;
using TMPro;
using UnityEngine.UI;

public struct WorldClickEvent : IEventParam
{
    public int WorldNumber { get; private set; }
    public Type WorldType { get; private set; }
    public WorldClickEvent(int worldNumber, Type type)
    {
        WorldNumber = worldNumber;
        WorldType = type;
    }
}
public class WorldCell : MonoBehaviour
{
    [SerializeField] private TMP_Text typeText = null;
    [SerializeField] private TMP_Text idText = null;
    [SerializeField] private Image image = null;
    public void Init(WorldInfo info)
    {
        idText.text = $"{ info.Id }Áö¿ª";
        typeText.text = $"{EnumTranslator.GetTypeString(info.WorldType)}";
        image.sprite = Resources.Load<Sprite>(info.Path);
        var button = GetComponentInChildren<Button>();
        button.SetClickListener(() =>
        {
            this.Emit(new WorldClickEvent(info.Id, info.WorldType));
        });
    }
}
