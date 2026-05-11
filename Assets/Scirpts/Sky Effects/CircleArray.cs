using UnityEditor;
using UnityEngine;
[ExecuteAlways]
public class CircleArray : MonoBehaviour
{
    [SerializeField] private float MinY = -80f;
    [SerializeField] private float MaxY = 80f;
    [SerializeField] private short Numbers = 15;
    [SerializeField] private CircleLine circleLine;

#if UNITY_EDITOR

    void OnValidate()
    {
        EditorApplication.delayCall -= Generate;
        EditorApplication.delayCall += Generate;
    }

    void OnDisable(){ EditorApplication.delayCall -= Generate; }

#endif

    void Start()
    {
        Generate();
    }

    private void Generate()
    {
        for(int index = transform.childCount - 1; index >= 0; index--)
        {
            if (Application.isEditor && !Application.isPlaying)
                DestroyImmediate(transform.GetChild(index).gameObject);
            else
                Destroy(transform.GetChild(index).gameObject);
        }
        if (circleLine == null) return;
        float len = MaxY - MinY;
        float deltaY = len/Numbers;
        for (int index = 0; index < Numbers; index++)
        {
            var cicle = Instantiate(circleLine,transform);
            cicle.transform.position = new Vector3(cicle.transform.position.x,MinY + index * deltaY,cicle.transform.position.z);
        }
    }
}
