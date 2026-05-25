using System.Collections;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float TimeToMove = 0.5f;
    [SerializeField] private float TTL = 4f;
    private Transform CannonMouth;
    private bool UpdateRotation = false;
    private float StartTime;

    void Start()
    {
        StartTime = Time.time;
        StartCoroutine(DestroyAfterTTL());
    }

    void Update()
    {
        if (Time.time - StartTime < TimeToMove) return;
        if (!UpdateRotation && CannonMouth != null)
        {
            UpdateRotation = true;
            transform.rotation = CannonMouth.rotation;
        }
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void setCannonMouth(Transform mouth)
    {
        CannonMouth = mouth;
        transform.rotation = mouth.rotation;
    }

    private IEnumerator DestroyAfterTTL()
    {
        yield return new WaitForSeconds(TTL);
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Shot trigger -> " + other.name);
        Drone drone = other.GetComponent<Drone>();
        if (drone != null) drone.TakeDamage(5);
    }
}
