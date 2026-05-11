using UnityEngine;

public class Wall : MonoBehaviour
{
    [Range(0,100)]
    [SerializeField] private ushort transparentIntesity;
    private Material material;
    private MeshRenderer meshRenderer;
    void Start()
    {
        material = GetComponent<Renderer>().material;
        meshRenderer = GetComponent<MeshRenderer>();
        MakeOpaque();
    }

    public void MakeTransparent()
    {
        Color color = material.color;
        color.a = 100f / (100f - transparentIntesity);
        material.color = color;
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }

    public void MakeOpaque()
    {
        Color color = material.color;
        color.a = 1f;
        material.color = color;
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }
}
