using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ComputerTabButton : MonoBehaviour
{
    private Slider _slider;
    [SerializeField] private float speed = 2f;
    [SerializeField] private Canvas desktop_UI;
    
    void OnEnable()
    {
        _slider = GetComponent<Slider>();
        _slider.value = 0f;
    }   

    void Update()
    {
        if (Keyboard.current[Key.Tab].isPressed)
            _slider.value = Mathf.Clamp(_slider.value + speed * Time.deltaTime, 0f, _slider.maxValue);
        else
            _slider.value = Mathf.Clamp(_slider.value - speed * Time.deltaTime, 0f, _slider.maxValue);
        
        if (_slider.value >= _slider.maxValue) Instantiate(desktop_UI,Vector3.zero,Quaternion.identity);
    }
}
