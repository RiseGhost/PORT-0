using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ComputerTabButton : MonoBehaviour
{
    private Slider _slider;
    [SerializeField] private float speed = 2f;
    [SerializeField] private Canvas desktop_UI;
    private GameObject DesktopUI = null;
    
    void OnEnable()
    {
        _slider = GetComponent<Slider>();
        _slider.value = 0f;
    }   

    void Update()
    {
        if (DesktopUI) return;
        if (Keyboard.current[Key.Tab].isPressed)
            _slider.value = Mathf.Clamp(_slider.value + speed * Time.deltaTime, 0f, _slider.maxValue);
        else
            _slider.value = Mathf.Clamp(_slider.value - speed * Time.deltaTime, 0f, _slider.maxValue);
        
        if (_slider.value >= _slider.maxValue)
        {
            DesktopUI = Instantiate(desktop_UI,Vector3.zero,Quaternion.identity).gameObject;
            _slider.value = 0f;
        }
    }
}
