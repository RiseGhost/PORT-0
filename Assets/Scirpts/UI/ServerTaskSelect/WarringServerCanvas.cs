using UnityEngine;

/*
    Description:
        This class control the canvas that is on top of the server (GameObject) and 
        that server as the waring indicates.
*/

public class WarringServerCanvas : MonoBehaviour
{
    void FixedUpdate()
    {
        if (Camera.main == null) return;
        transform.rotation = Camera.main.transform.rotation;
    }
}
