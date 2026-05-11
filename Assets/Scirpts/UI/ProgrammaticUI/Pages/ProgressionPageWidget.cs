using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionPageWidget : MonoBehaviour
{
    [SerializeField] private Texture2D completed, not_completed;
    [SerializeField] private Slider slider;
    [Range(0, 100)]
    [SerializeField] private float StartValue;
    public bool LoackValue = false;
    [Range(0, 100)]
    private byte completedValue;
    private float currentValue;

    void Start()
    {
        if (slider == null) Destroy(this);
        slider.value = StartValue;
    }

    void Update()
    {
        if (LoackValue) return;
        currentValue = Mathf.MoveTowards(currentValue, completedValue, 35f * Time.deltaTime);
        slider.value = currentValue;
    }

    public void IncrementValue(byte value)
    {
        if (value > 100) completedValue = 100;
        else if (completedValue + value > 100) completedValue = 100;
        else completedValue += value;
    }
    
    public void DecrementValue(byte value)
    {
        if (completedValue < value) completedValue = 0;
        else completedValue -= value;
    }
}
