using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class RawImageColorLerp : MonoBehaviour
{
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;
    [SerializeField] private float speed = 1f;
    private RawImage _rawImage;

    private void Awake()
    {
        _rawImage = GetComponent<RawImage>();
        Debug.Log("RawImageColorLerp component added to " + gameObject.name);
    }

    void Update()
    {
        _rawImage.color = Color.Lerp(_rawImage.color, _endColor, (speed * Time.deltaTime));
    }

    public void setColors(Color startColor, Color endColor)
    {
        _startColor = startColor;
        _endColor = endColor;
        _rawImage.color = _startColor;
    }

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
