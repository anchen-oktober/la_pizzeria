using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RandomCustomerBehavior : MonoBehaviour
{
    public Transform[] waypoints;
    public float waitTime = 4f;

    private int currentIndex = 0;
    private NavMeshAgent agent;
    private Animator animator;
    private bool isWaiting = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        agent.autoBraking = true;
        GoToNextPoint();
    }

    void Update()
    {
        bool moving = agent.velocity.magnitude > 0.1f;
        animator.SetBool("isMoving", moving);

        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);

        if (!agent.pathPending && agent.remainingDistance < 0.2f && !isWaiting)
        {
            if (currentIndex == waypoints.Length - 1)
            {
                StartCoroutine(WaitAtPoint());
            }
            else
            {
                currentIndex++;
                agent.SetDestination(waypoints[currentIndex].position);
            }
        }
    }

    void GoToNextPoint()
    {
        if (waypoints.Length == 0) return;

        agent.isStopped = false;
        agent.destination = waypoints[currentIndex].position;
    }

    IEnumerator WaitAtPoint()
    {

        isWaiting = true;
        agent.isStopped = true;

        yield return new WaitForSeconds(waitTime);

        currentIndex = (currentIndex + 1) % waypoints.Length;
        isWaiting = false;
        GoToNextPoint();
    }
}