using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FilterButton : MonoBehaviour
{
    [SerializeField] private Button filterButton = null;
    [SerializeField] private TextMeshProUGUI text = null;
    //[SerializeField] private FilterTable filterTable = null;
    void Awake()
    {
        filterButton.SetClickListener(ShowTable);
    }

    private void ShowTable()
    {
        //TODO
        //filterTable
        //Filter.·¹¾îµµ.ToString();
    }
}
