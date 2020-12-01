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
         Debug.Log("asdf");
         agent.SetDestination(target.transform.position);
    }
}
