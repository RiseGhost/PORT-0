using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class CylinderLines : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float radius = 10f;
    [SerializeField] private short LinesNumber = 15;

# if UNITY_EDITOR
    void OnValidate()
    {
        EditorApplication.delayCall -= Generate;
        EditorApplication.delayCall += Generate;
    }

    void OnDisable()
    {
        EditorApplication.delayCall -= Generate;
    }
#endif

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        for (int index = transform.childCount - 1; index >= 0; index--)
        {
            if (Application.isEditor && !Application.isPlaying)
                DestroyImmediate(transform.GetChild(index).gameObject);
            else
                Destroy(transform.GetChild(index).gameObject);
        }
        if (lineRenderer == null) return;
        DrawLines();
    }

    private void DrawLines()
    {
        float angleStep = 360f/LinesNumber;
        for (short index = 0; index < LinesNumber; index++)
        {
            float angle = Mathf.Deg2Rad * angleStep * index;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            var line = Instantiate(lineRenderer,transform).GetComponent<LineRenderer>();
            line.positionCount = 2;
            line.SetPosition(0,new Vector3(x,-80f,z) + transform.position);
            line.SetPosition(1,new Vector3(x,80f,z) + transform.position);
        }
    }
}
