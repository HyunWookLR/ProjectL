using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FilterPopup : MonoBehaviour
{
    [SerializeField] private Toggle toggle = null;
    [SerializeField] private TextMeshProUGUI text = null;
    //[SerializeField] private FilterTable filterTable = null;
    void Awake()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        //toggle.isOn = false;
        //toggle.SetClickListener();

        //TODO 자식들도 forceRebuild하고 그 전에 스크롤 크기 다시 재야함
    }

    private void ShowTable()
    {
        //TODO
        //filterTable
        //Filter.레어도.ToString();
    }
}
