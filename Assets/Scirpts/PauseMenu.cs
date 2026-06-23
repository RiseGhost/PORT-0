using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private Canvas canvas;
    private bool visible = false;

    void Awake()
    {
        Debug.Log("Pause Menu: is ready ✅");
        this.name = "Pause Menu System";
    }

    void Start()
    {
        canvas = Resources.Load<Canvas>("UI/Pause Menu");
        if (canvas == null) Destroy(this.gameObject);
        canvas = Instantiate(canvas,Vector3.zero,Quaternion.identity);
        canvas.name = "Pause Menu Canvas";
        canvas.gameObject.SetActive(false);
        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (!SceneManager.GetActiveScene().name.Equals("Game")){
            visible = false;
            return;
        }
        if (Keyboard.current[Key.Escape].wasPressedThisFrame) visible = !visible;
        if (Keyboard.current[Key.M].wasPressedThisFrame)
        {
            visible = false;
            Time.timeScale = 1;
            SceneManager.LoadScene("HomeMenu");
        }
        if (Keyboard.current[Key.Q].wasPressedThisFrame) Application.Quit();
        canvas.gameObject.SetActive(visible);
        if (visible) Time.timeScale = 0f;
        else Time.timeScale = 1f; 
    }
}
