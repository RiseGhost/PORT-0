using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed = 5f;
    [SerializeField] private DroneUILook uiLook;
    [SerializeField] private float health = 10f;
    private Transform currentWaypoint = null;

    void Update()
    {
        MoveTowardsWaypoint();
    }

    public DroneUILook GetUILook(){ return uiLook; }

    private Transform GetNextWaypoint()
    {
        if (currentWaypoint == null)
            return waypoints[0];

        int currentIndex = System.Array.IndexOf(waypoints, currentWaypoint);
        int nextIndex = (currentIndex + 1) % waypoints.Length;
        return waypoints[nextIndex];
    }

    protected void MoveTowardsWaypoint()
    {
        if (currentWaypoint == null)
            currentWaypoint = GetNextWaypoint();

        Vector3 direction = (currentWaypoint.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.LookAt(currentWaypoint);

        if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.1f)
            currentWaypoint = GetNextWaypoint();
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("O drone tomou dano");
        health -= damage;
        if (health <= 0)
            Destroy(gameObject);
    }
}
