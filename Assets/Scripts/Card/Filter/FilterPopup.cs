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

        //TODO �ڽĵ鵵 forceRebuild�ϰ� �� ���� ��ũ�� ũ�� �ٽ� �����
    }

    private void ShowTable()
    {
        //TODO
        //filterTable
        //Filter.���.ToString();
    }
}
