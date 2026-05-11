using UnityEditor;
using UnityEngine;

public class AlreadyShow : MonoBehaviour
{
    private Transform CameraTransform;
    private Wall LastWall;
    
    void Update()
    {
        if (Camera.main == null) return;
        CameraTransform = Camera.main.transform;
        Vector3 direction = transform.position - CameraTransform.position;
        RaycastHit[] hits = Physics.RaycastAll(new Ray(CameraTransform.position,direction),direction.magnitude);
        if (LastWall != null) LastWall.MakeOpaque();
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.GetComponent<Wall>() != null)
            {
                LastWall = hit.collider.GetComponent<Wall>();
                LastWall.MakeTransparent();
            }
        }
        Debug.DrawLine(CameraTransform.position, CameraTransform.position + direction, Color.yellow);
    }
}
