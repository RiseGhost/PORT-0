using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SoundEffectLabel : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private TextMeshProUGUI _text;
    void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        if (_slider == null) Destroy(this);
    }

    void Update()
    {
        float newvalue = _slider.value + Mathf.Abs(_slider.minValue);
        float maxPrecent = _slider.maxValue + Mathf.Abs(_slider.minValue);
        float precent = (newvalue * 100f) / maxPrecent;
        _text.text = ((precent < 100f) ? " " : "") + precent.ToString("F1") + " %";
    }
}
