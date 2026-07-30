using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JumpCutScene : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float speed = 4f;
    private float value = 0f;

    void Update()
    {
        if (Keyboard.current[Key.Tab].isPressed)    value += speed * Time.deltaTime;
        else                                        value -= speed * 2f * Time.deltaTime;
        value = Mathf.Clamp(value,0f,100f);
        slider.value = value;
        if (value == 100f) SceneManager.LoadScene("HomeMenu");
    }
}
