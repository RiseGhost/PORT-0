using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField] private int Points = 3;
    [SerializeField] private float speed = 5f;
    [SerializeField] private DroneUILook uiLook;
    [SerializeField] private float health = 10f;
    private List<Transform> waypoints = new List<Transform>();
    private Transform currentWaypoint = null;
    private EnemyTakeBlink[] Blinks;

    void Start()
    {
        WayPoints wayPoints = GameObject.FindAnyObjectByType<WayPoints>();
        if (wayPoints == null) return;
        this.waypoints = wayPoints.GetPoints(Points);
        Blinks = GetComponentsInChildren<EnemyTakeBlink>();
    }

    void Update()
    {
        MoveTowardsWaypoint();
    }

    public DroneUILook GetUILook(){ return uiLook; }

    private Transform GetNextWaypoint()
    {
        if (currentWaypoint == null)
            return waypoints[0];

        int currentIndex = waypoints.IndexOf(currentWaypoint);
        int nextIndex = (currentIndex + 1) % waypoints.Count;
        return waypoints[nextIndex];
    }

    protected void MoveTowardsWaypoint()
    {
        if (waypoints.Count == 0) return;
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
        if (Blinks != null)
        {
            foreach (var b in Blinks)
            {
                b.Blink();
            }
        }
        health -= damage;
        if (health <= 0)
            Destroy(gameObject);
    }
}
