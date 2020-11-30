using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passanger : MonoBehaviour
{
    public Animator animator;
    public Transform EndPoint1;
    public Transform EndPoint2;

    public bool atStation = true;

    int status = 2;
    public static bool isWalking = false;
    bool HasBeen = true;
    bool GotToFirstPoint = false;
    bool reachedStandingPoint = false;
    Transform ClosestPoint;
    bool onTrain = false;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        /*
        if (!onTrain)
        {

            if (reachedStandingPoint)
            {
                //transform.position = currentStandingPoint.position;
                gameObject.transform.SetParent(Train.instance.gameObject.transform, true);
                isWalking = false;
                onTrain = true;
                reachedStandingPoint = false;
                status = 2;
                PassangerController.EnableOrDisableAgent(false);
                return;
            }
        }
        if (isWalking)
        {
            //reachedStandingPoint = false;
            isWalking = false;
            status = 1;

        }
        switch (status)
        {
            case 0:
                if (HasBeen)
                {
                    PassangerController.MoveAgent(EndPoint1);
                    onTrain = false;
                    //Walk(EndPoint1);
                }
                else
                {
                    PassangerController.MoveAgent(EndPoint2);
                    onTrain = false;
                    //Walk(EndPoint2);
                }
                HasBeen = !HasBeen;
                break;
            case 1:

                if (GotToFirstPoint == false)
                {
                    ClosestPoint = WagonManager.Points[0];

                    foreach (Transform point in WagonManager.Points)
                    {
                        if (Vector3.Distance(transform.position, point.position) < Vector3.Distance(transform.position, ClosestPoint.position))
                        {
                            ClosestPoint = point;
                        }
                    }
                    if (Vector3.Distance(transform.position, ClosestPoint.position) <= 2f)
                    {
                        GotToFirstPoint = true;
                    }
                }

                if (GotToFirstPoint)
                {
                    ClosestPoint = WagonManager.StandingPoints[0];

                    foreach (Transform point in WagonManager.StandingPoints)
                    {
                        if (Vector3.Distance(transform.position, point.position) <= Vector3.Distance(transform.position, ClosestPoint.position))
                        {
                            ClosestPoint = point;
                            //Debug.Log(Vector3.Distance(transform.position, ClosestPoint.position));
                        }
                    }

                    if (Vector3.Distance(transform.position, ClosestPoint.position) <= 2.5f)
                    {
                        reachedStandingPoint = true;
                        isWalking = false;
                        break;
                    }

                }
                PassangerController.MoveAgent(ClosestPoint);
                //Walk(ClosestPoint);

                break;
            default:
                animator.SetBool("Walk", false);
                break;
        }*/
    }
    public void Walk(Transform point)
    {
        animator.SetBool("Walk", true);
        Vector3 dir = point.position - transform.position;
        transform.Translate(dir.normalized * 1f * Time.deltaTime, Space.World);
        transform.LookAt(point);
    }
}
