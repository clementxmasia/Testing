using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class WanderNavMesh : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 destination;
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;
    public float movementSpeed = 3.5f;

    private float timer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;

        SetRandomDestination();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SetRandomDestination();
            timer = wanderTimer;
        }

        agent.speed = movementSpeed;
    }

    private void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1);
        destination = hit.position;
        agent.SetDestination(destination);
    }
}
