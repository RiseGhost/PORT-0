using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialShop : MonoBehaviour
{

    void Update()
    {
        if (Keyboard.current[Key.F].wasPressedThisFrame)
        {
            Destroy(this.gameObject);
        }
    }
}
