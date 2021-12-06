using UnityEngine;
using UnityEngine.UI;
public class CatalogCard : MonoBehaviour
{
    [SerializeField] private Button button = null;
    [SerializeField] private RectTransform rectTransform = null;
    [SerializeField] private Image characterImage = null;
    [SerializeField] GameObject blockingImage = null;
    public float Width { get => rectTransform.rect.width; }
    public float Height { get => rectTransform.rect.height; }
    public Vector2 Position { get => rectTransform.anchoredPosition; }

    public void Init(Vector2 position, CardInfo cardInfo, bool hasAcheived)
    {
        blockingImage.SetActive(!hasAcheived);
        rectTransform.anchoredPosition = position;
        characterImage.sprite = Resources.Load<Sprite>($"Sprite/Card/CardChar/character{cardInfo.Id}");

        button.SetClickListener(()=> 
        {
            Instantiate(Resources.Load($"Prefab/Card/Popup/CatalogCardPopup"), transform);
            //TODO
            //카드 설명팝업 만들기+부모객체 밖으로 
        });
    }
    public void RePosition(Vector2 position) => rectTransform.anchoredPosition = position;
}
