using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class SliderHandleZoomAnimator : MonoBehaviour
{
    private Animator _animator;
    private bool focus = false;
    void Start(){
        _animator = GetComponent<Animator>();
        if (_animator == null) Destroy(this);
    }

    void Update(){
        _animator.SetBool("Focus",focus);
    }

    public void changeFocus(bool focus)
    {
        this.focus = focus;
    }
}
