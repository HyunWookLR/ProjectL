using UnityEngine;

public class SafeArea : MonoBehaviour
{
    private RectTransform mainRect;
    private Rect basicSafeArea = Rect.zero;
    void Start()
    {
        mainRect = GetComponent<RectTransform>();
        Set();
    }

    private void Set()
    {
        if(Screen.safeArea != basicSafeArea)
        {
            basicSafeArea = Screen.safeArea;
            Vector2 anchorMax = basicSafeArea.position + basicSafeArea.size;
            Vector2 anchorMin = basicSafeArea.position;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            mainRect.anchorMax = anchorMax;
            mainRect.anchorMin = anchorMin;
        }
    }
}
