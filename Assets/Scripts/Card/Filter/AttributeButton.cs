using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttributeButton : MonoBehaviour
{
    public enum AttributeType
    {
        Color,
        Rareness
    }
    [SerializeField] private AttributeType type;
    [SerializeField] private Button attributeButton = null;
    [SerializeField] private TextMeshProUGUI text = null;
    void Awake()
    {
        attributeButton.SetClickListener(ShowTable);
    }

    private void ShowTable()
    {

    }
}
