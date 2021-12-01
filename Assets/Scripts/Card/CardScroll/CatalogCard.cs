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
    public void Init(Vector2 position, CardInfo cardInfo, bool hasAcheived)
    {
        blockingImage.SetActive(!hasAcheived);
        rectTransform.anchoredPosition = position;
        characterImage.sprite = Resources.Load<Sprite>($"Sprite/Card/CardChar/character{cardInfo.Id}");
        
        button.SetClickListener(()=> 
        {
            Instantiate(Resources.Load($"Prefab/CatalogCardPopup"), transform);
        });
    }

}
