using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationActivator : MonoBehaviour
{
    public static bool OutSideObjectActivator = false;

    public Transform Doors1Activator;
    public Transform Doors2Activator;

    public Animator Doors1Animator;
    public Animator Doors2Animator;

    // boolean for managing the doors.
    bool open = true;

    void Update()
    {
        // I chose pressing "space" button just for example.
        // You can make the trasintions in any other way that you want to.
        // Just call the function below to control it.
        if (OutSideObjectActivator)
        {
            if (open)
            {
                ForceOpenDoors();
                OutSideObjectActivator = false;
            }
            else
            {
                ForceCloseDoors();
                OutSideObjectActivator = false;
            }
            open = !open;
        }
    }

    // You call this function when you want to open the doors.
    public void ForceOpenDoors()
    {
        Doors1Animator.SetBool("Open", true);
        Doors2Animator.SetBool("Open", true);
    }

    // You call this function when you want to close the doors.
    public void ForceCloseDoors()
    {
        Doors1Animator.SetBool("Open", false);
        Doors1Animator.SetBool("Idle", true);
        Doors2Animator.SetBool("Open", false);
        Doors2Animator.SetBool("Idle", true);
    }

}
