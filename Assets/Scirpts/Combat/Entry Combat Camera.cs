using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class EntryCombatCamera : MonoBehaviour
{
    [SerializeField] private Camera Entry_Camera_Combat;
    private Transform origin_Transform;
    private Animator _animator;

    void Start(){
        if (Entry_Camera_Combat == null) Destroy(this.gameObject);
        _animator = Entry_Camera_Combat.GetComponent<Animator>();
        if (_animator == null) Destroy(this.gameObject);
        origin_Transform = Entry_Camera_Combat.transform;
        Entry_Camera_Combat.gameObject.SetActive(false);
    }

    void Update()
    {
        if (PlayerController.Lock) return;
        EventSystem eventSystem = EventSystem.current;
    }

    public void DDosLaunch()
    {
        Entry_Camera_Combat.gameObject.SetActive(true);
        Entry_Camera_Combat.transform.position = origin_Transform.position;
        Entry_Camera_Combat.transform.rotation = origin_Transform.rotation;
        _animator.SetTrigger("DDos");
    }

    public void ResetCamera()
    {
        Entry_Camera_Combat.transform.position = origin_Transform.position;
        Entry_Camera_Combat.transform.rotation = origin_Transform.rotation;
        Entry_Camera_Combat.gameObject.SetActive(false);
    }
}
