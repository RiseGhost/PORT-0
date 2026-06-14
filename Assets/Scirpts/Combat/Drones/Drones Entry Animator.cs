using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DronesEntryAnimator : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Dance()
    {
        _animator.SetTrigger("Dance");
    }

    //Called by animation event
    public void OnDanceFinish()
    {
        GameObject.FindAnyObjectByType<EntryCombatCamera>().ResetCamera();
    }
}
