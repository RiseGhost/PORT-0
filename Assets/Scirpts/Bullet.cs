using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float ttl = 2.5f;
    private Rigidbody _rigidbody;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.linearVelocity = transform.forward * speed;
        StartCoroutine(Kill());
    }
    
    private IEnumerator Kill()
    {
        yield return new WaitForSeconds(ttl);
        Destroy(gameObject);
    }
}
