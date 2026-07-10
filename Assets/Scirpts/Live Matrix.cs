using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class LiveMatrix : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Vector2 _mousePos = Vector2.zero;
    private Material _material;

    void Start()
    {
        _material = GetComponent<RawImage>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouse = Mouse.current.position.ReadValue();
        if (_mousePos == Vector2.zero)  _mousePos = new Vector2(mouse.x/Screen.width,mouse.y/Screen.height);
        else                            _mousePos = Vector2.Lerp(_mousePos,new Vector2(mouse.x/Screen.width,mouse.y/Screen.height),Time.deltaTime * speed);
        _material.SetVector("_Circle_Pos",new Vector4(_mousePos.x,_mousePos.y,0,0));
    }
}
