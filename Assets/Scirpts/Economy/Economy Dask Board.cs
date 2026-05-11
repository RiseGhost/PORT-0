using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EconomyDaskBoard : MonoBehaviour
{
    [SerializeField] private Canvas DaskBoard;
    [SerializeField] private Key key = Key.Q;
    private bool visible = false;
    
    void Start()
    {
        if (DaskBoard == null) Destroy(gameObject);
    }

    void Update()
    {
        Scene scene = SceneManager.GetSceneByName("WordRush");
        if (scene.isLoaded) return;
        DaskBoard.gameObject.SetActive(visible);
        if (Keyboard.current[key].wasPressedThisFrame) visible = !visible;
    }
}
