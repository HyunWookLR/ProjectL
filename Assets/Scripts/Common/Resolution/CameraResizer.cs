using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraResizer : MonoBehaviour
{
    void Start()
    {
        float targetRatio = 9f / 16f;
        float windowRatio = (float)Screen.width / (float)Screen.height;
        var preferHeightRatio = windowRatio / targetRatio;

        if(preferHeightRatio > 1f )
        {
            Camera camera = GetComponent<Camera>();
            Rect rect = camera.rect;

            float preferWidthRatio = 1f / preferHeightRatio;

            rect.width = preferWidthRatio;
            rect.height = 1f;
            rect.x = (1f - preferWidthRatio) / 2f;
            rect.y = 0f;

            camera.rect = rect;
        }
    }
}
