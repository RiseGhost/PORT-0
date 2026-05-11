using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AnimatorWalking : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Rigidbody _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (animator == null)
            Destroy(gameObject);
    }

    void Update()
    {
        float speedx = _rigidbody.linearVelocity.x;
        float speedz = _rigidbody.linearVelocity.z;
        animator.SetBool("Walking", Mathf.Abs(speedx) > 0.2f || Mathf.Abs(speedz) > 0.2f);
    }
}
