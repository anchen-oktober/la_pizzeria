using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class LorenzoBehavior : MonoBehaviour
{
    public Transform[] waypoints;
    public float waitTime = 4f;

    private int currentIndex = 0;
    private NavMeshAgent agent;
    private Animator animator;
    private bool isWaiting = false;

    //void Start()
    //{
    //    agent = GetComponent<NavMeshAgent>();
    //    animator = GetComponentInChildren<Animator>();

    //    agent.autoBraking = true;
    //    GoToNextPoint();
    //}

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        if (agent.isOnNavMesh)
        {
            agent.autoBraking = true;
            GoToNextPoint();
        }
        else
        {
            Debug.LogWarning("Lorenzo is not on NavMesh!");
        }
    }

    //void Update()
    //{
    //    bool moving = agent.velocity.magnitude > 0.1f;
    //    animator.SetBool("isMoving", moving);

    //    float speed = agent.velocity.magnitude;
    //    animator.SetFloat("Speed", speed);

    //    if (!agent.pathPending && agent.remainingDistance < 0.2f && !isWaiting)
    //    {
    //        if (currentIndex == waypoints.Length - 1)
    //        {
    //            StartCoroutine(WaitAtPoint());
    //        }
    //        else
    //        {
    //            currentIndex++;
    //            agent.SetDestination(waypoints[currentIndex].position);
    //        }
    //    }
    //}

    void Update()
    {
        bool moving = agent.velocity.magnitude > 0.1f;
        animator.SetBool("isMoving", moving);
        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (!agent.pathPending && agent.remainingDistance < 0.2f && !isWaiting)
        {
            StartCoroutine(WaitAtPoint());
        }
    }

    void GoToNextPoint()
    {
        if (waypoints.Length == 0) return;

        agent.isStopped = false;
        agent.destination = waypoints[currentIndex].position;
    }

    //IEnumerator WaitAtPoint()
    //{

    //    isWaiting = true;
    //    agent.isStopped = true;

    //    yield return new WaitForSeconds(waitTime);

    //    currentIndex = (currentIndex + 1) % waypoints.Length;
    //    isWaiting = false;
    //    GoToNextPoint();
    //}

    IEnumerator WaitAtPoint()
    {
        isWaiting = true;
        agent.isStopped = true;

        yield return new WaitForSeconds(waitTime);

        currentIndex = (currentIndex + 1) % waypoints.Length;
        //currentIndex = Random.Range(0, waypoints.Length); - for random points

        agent.isStopped = false;
        GoToNextPoint();

        isWaiting = false;
    }

    void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] != null)
                Gizmos.DrawSphere(waypoints[i].position, 0.2f);
        }
    }
}