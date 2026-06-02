using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EconomyDaskBoard : MonoBehaviour
{
    [SerializeField] private Canvas DaskBoard;
    [SerializeField] private Key key = Key.Q;
    private bool visible = false;

    void OnDisable()
    {
        if (DaskBoard != null) DaskBoard.gameObject.SetActive(false);
    }

    void Start()
    {
        if (DaskBoard == null) Destroy(gameObject);
    }

    void Update()
    {
        if (PlayerController.Lock) return;
        EventSystem eventSystem = EventSystem.current;
        if (eventSystem != null && eventSystem.currentSelectedGameObject != null) return;
        DaskBoard.gameObject.SetActive(visible);
        if (Keyboard.current[key].wasPressedThisFrame) visible = !visible;
    }
}
