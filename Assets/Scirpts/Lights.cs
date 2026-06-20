using UnityEngine;

public class Lights : MonoBehaviour
{
    private Light _light;
    private MeshRenderer _meshRender;

    void Start()
    {
        _light = GetComponent<Light>();
        _meshRender = GetComponent<MeshRenderer>();
    }

    public void Deactivate()
    {
        if (_light != null)         _light.enabled = false;
        if (_meshRender != null)    _meshRender.enabled = false;
    }

    public void Active()
    {
        if (_light != null)         _light.enabled = true;
        if (_meshRender != null)    _meshRender.enabled = true;
    }
}
