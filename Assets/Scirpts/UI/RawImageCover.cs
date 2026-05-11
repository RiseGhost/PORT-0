using UnityEngine;
using UnityEngine.UI;

public class RawImageCover : MonoBehaviour
{
    private RawImage rawImage;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
        if (rawImage == null) return;
        float size = (Screen.width > Screen.height) ? Screen.width : Screen.height;
        rawImage.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
        rawImage.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}