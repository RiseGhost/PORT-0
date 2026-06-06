using UnityEngine;
using UnityEngine.InputSystem;

public class ComputerUI : MonoBehaviour
{
    void OnEnable()
    {
        PlayerController.Lock = true;
        CameraFollow.LockRotate();
        TaskServer.Lock = true;
    }

    void OnDisable()
    {
        PlayerController.Lock = false;
        CameraFollow.UnlockRotate();
        TaskServer.Lock = false;
    }

    void OnDestroy()
    {
        PlayerController.Lock = false;
        CameraFollow.UnlockRotate();
        TaskServer.Lock = false;
    }

    void Update()
    {
        if (Keyboard.current[Key.Tab].wasPressedThisFrame) Destroy(this.gameObject);
    }
}
