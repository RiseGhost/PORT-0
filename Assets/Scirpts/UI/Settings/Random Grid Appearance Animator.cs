using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class RandomGridAppearanceAnimator : MonoBehaviour
{
    public float speed = 1f;
    public bool autoDestroy = true;
    private Material _material;
    void Start(){
        RawImage image = GetComponent<RawImage>();
        _material = new Material(image.material);
        if (_material == null) Destroy(this);
        image.material = _material;
        _material.SetFloat("_Progress",0f);
    }

    void OnDestroy()
    {
        _material.SetFloat("_Progress",0f);
    }

    void Update(){
        _material.SetFloat("_Progress",Mathf.MoveTowards(_material.GetFloat("_Progress"),1,Time.deltaTime * speed));
        if (_material.GetFloat("_Progress") >= 1f && autoDestroy) Destroy(this);
    }
}
