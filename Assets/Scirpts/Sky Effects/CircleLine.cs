using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(LineRenderer))]
public class CircleLine : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    [Range(3f,300f)]
    [SerializeField] private short segments;
    [SerializeField] private float width = 0.1f;
    private LineRenderer lr;

    void OnValidate()
    {
        Start();
    }

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = false;
        lr.loop = true;
        lr.positionCount = segments;
        lr.startWidth = width;
        lr.endWidth = width;
        DrawCircle();
    }

    void DrawCircle()
    {
        float angleSteps = 360f/segments;
        for (int i = 0; i < segments; i++)
        {
            float angle = Mathf.Deg2Rad * angleSteps * i;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            lr.SetPosition(i, new Vector3(x,0,z));
        }
    }
}
