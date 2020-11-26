using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PassangerController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public static NavMeshAgent staticAgent;

    void Start()
    {
        staticAgent = agent;
    }
    public static void EnableOrDisableAgent(bool active)
    {
        staticAgent.enabled = active;
    }
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;
    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            agent.SetDestination(hit.point);
    //        }
    //    }
    //}
    public static void MoveAgent(Transform point)
    {
        staticAgent.SetDestination(point.position);
    }
}
