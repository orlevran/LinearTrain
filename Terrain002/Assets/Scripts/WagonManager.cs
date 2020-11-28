using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonManager : MonoBehaviour
{
    public static List<Transform> Points;
    public static List<Transform> StandingPoints;

    public Transform Point1;
    public Transform Point2;
    public Transform StandingPoint;

    void Start()
    {
        Points = new List<Transform>();
        StandingPoints = new List<Transform>();
        if (Point1 != null && Point2 != null)
        {
            Points.Add(Point1);
            Points.Add(Point2);
        }
        if (StandingPoint != null)
        {
            StandingPoints.Add(StandingPoint);
        }
    }
}
