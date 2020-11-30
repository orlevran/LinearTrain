using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CapsuleManager : MonoBehaviour
{
    public GameObject target;
    public GameObject train;
    public GameObject trainStopPos;

    public NavMeshAgent agent;

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(trainStopPos.transform.position, train.transform.position) < 1)
        {
            agent.SetDestination(target.transform.position);
        }
    }
}
