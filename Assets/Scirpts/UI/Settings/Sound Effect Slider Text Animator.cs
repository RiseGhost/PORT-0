using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SoundEffectSliderTextAnimator : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();
    [SerializeField] private Color SelectColor = Color.white;
    [SerializeField] private Color DefaultColor = Color.black;
    [SerializeField] private float speed = 1f;
    void Update()
    {
        EventSystem eventSystem = EventSystem.current;
        if (eventSystem == null) return;
        GameObject selected = eventSystem.currentSelectedGameObject;
        foreach(TextMeshProUGUI text in texts)
        {
            if (text == null) continue;
            text.color = (selected == gameObject) ? Color.Lerp(text.color,SelectColor,Time.deltaTime) : Color.Lerp(text.color,DefaultColor,Time.deltaTime);
        }
    }
}
