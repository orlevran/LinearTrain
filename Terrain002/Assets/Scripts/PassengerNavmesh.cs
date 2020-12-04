using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PassengerNavmesh : MonoBehaviour
{
    public NavMeshAgent agent;
    public List<Transform> path = new List<Transform>();
    public int pathIndex = 0;

    public WagonManager wagon;
    public Transform StandingPoint;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // manage the agent and his path follow
        if (path.Count > 0)
        {
            animator.SetTrigger("Walk");
            if (!agent.enabled)
                agent.enabled = true;
            agent.SetDestination(path[pathIndex].position);
            if (Vector3.Distance(transform.position, path[pathIndex].position) < 2)
            {
                if (pathIndex < path.Count-1)
                    pathIndex++;
                else // path ended - reset the path
                {
                    animator.ResetTrigger("Walk");
                    path.Clear();
                    pathIndex = 0;
                    agent.enabled = false;
                }
            }
        }

    }

}
