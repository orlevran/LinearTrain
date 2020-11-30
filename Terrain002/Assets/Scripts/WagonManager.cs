using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonManager : MonoBehaviour
{
    public List<Transform> StandingPoints = new List<Transform>();
    private int capacity;
    private int currentCapacity = 0;

    void Start()
    {
        capacity = (int)Random.Range(2, 4);
    }

    public void FillWagon()
    {
        currentCapacity++;
    }

    public void UnloadWagon()
    {
        currentCapacity = 0;
    }

    public bool checkAvailability()
    {
        if (currentCapacity < capacity)
            return true;
        return false;
    }

    // returns a point to stand at
    public Transform GetStandingPoint()
    {
        return StandingPoints[(int)Random.Range(0, 1)];
    }
}
