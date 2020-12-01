using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonManager : MonoBehaviour
{
    public List<Transform> StandingPoints = new List<Transform>();
    public Transform Entrance;
    public Transform Center;


    // returns a point to stand at
    public Transform GetStandingPoint()
    {
        return StandingPoints[(int)Random.Range(0, 2)];
    }
}
