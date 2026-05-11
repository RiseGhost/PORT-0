using UnityEngine;
using UnityEngine.AI;

public class Aspirador : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private float Raio;
    [SerializeField] private float Time_UP_Pos = 3f;
    [SerializeField] private Vector3 angle_ajust;
    private Vector3 pos;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pos = randomMove();
    }

    void Update()
    {
        if (agent.remainingDistance < 1f) pos = randomMove();
    }

    private Vector3 randomMove()
    {
        Vector3 direction = Random.onUnitSphere;
        if (direction.magnitude > 0.4f) direction *= 2f;
        Vector3 pos = transform.position + direction * Raio;
        pos = new Vector3(pos.x,0f,pos.z);
        if (pos.x > 11f || pos.x < 11f) pos = new Vector3(Random.Range(-12f,12f),0f,pos.z);
        agent.updateRotation = true;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(pos, out hit, 2f, NavMesh.AllAreas))
        {
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(hit.position,path);

            if (path.status == NavMeshPathStatus.PathComplete)
            {
                agent.SetDestination(hit.position);
                return pos;
            }
            else return randomMove();
        }
        else return Vector3.zero;
    }
}
