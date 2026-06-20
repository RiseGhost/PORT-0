using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MatrixLines : MonoBehaviour
{
    private LineRenderer _render;
    private Material defaultMaterial;
    [SerializeField] private Material No_Energy;
    
    void Start()
    {
        _render = GetComponent<LineRenderer>();
        defaultMaterial = _render.material;
    }

    void Update()
    {
        if (No_Energy != null) _render.material = (PowerSupply.Exist_Energy()) ? defaultMaterial : No_Energy;
    }
}
