using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum cameraType { Main, ServerSelect, Combat }

[System.Serializable]
public struct CameraEntry
{
    public Camera camera;
    public cameraType type;
}

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private CameraEntry[] cameras;
    private Camera Focus_Camera;
    private int index = 0;

    void Start()
    {
        for(short i = 1; i < cameras.Length; i++)
        {
            cameras[i].camera.gameObject.SetActive(false);
        }

        List<CameraEntry> focus = cameras.Where(x => x.type == cameraType.ServerSelect).ToList();
        if (focus.Count == 0) return;
        Focus_Camera = focus.First().camera;
    }

    void Update()
    {
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null)
            return;
        if (Keyboard.current[Key.C].wasPressedThisFrame) Switch();
    }

    public Camera Switch()
    {
        cameras[index].camera.gameObject.SetActive(false);
        index = (index + 1) % cameras.Length;
        cameras[index].camera.gameObject.SetActive(true);
        return cameras[index].camera;
    }

    public Camera Switch_Focus_Camera()
    {
        if (Focus_Camera == null) return null;
        cameras.First().camera.gameObject.SetActive(false);
        Focus_Camera.gameObject.SetActive(true);
        return Focus_Camera;
    }

    public Camera Switch_Combat_Camera()
    {
        Camera combatCamera = cameras.Where(x => x.type == cameraType.Combat).ToList().First().camera;
        if (combatCamera == null) return null;
        combatCamera.gameObject.SetActive(true);
        cameras.Where(x => x.type == cameraType.Main).ToList().First().camera.gameObject.SetActive(false);
        return combatCamera;
    }

    public void Switch_main_camera()
    {
        cameras.Where(x => x.type == cameraType.Main).ToList().First().camera.gameObject.SetActive(true);
        if (Focus_Camera == null) return;
        Focus_Camera.gameObject.SetActive(false);
        cameras.Where(x => x.type != cameraType.Main).ToList().ForEach(x => x.camera.gameObject.SetActive(false));
    }
}
