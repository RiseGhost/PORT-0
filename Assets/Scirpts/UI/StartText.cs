using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartText : MonoBehaviour
{
    private string text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>().text;
    }

    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = (Keyboard.current[Key.Space].isPressed) ? "" : text;
    }
}