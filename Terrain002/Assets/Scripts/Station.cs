using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    public GameObject passengerPrefab;
    public Transform EndPoint;
    public Transform StationCenter;
    public int numOfPassengers;

    public List<GameObject> passengers = new List<GameObject>();

    public void SpawnPassengers()
    {
        // spawn numOfPassengers in the station.
        for (int i = 0; i < numOfPassengers; i++)
        {
            Vector3 startingPoint = StationCenter.transform.position + new Vector3(Random.Range(-20, -20), 0, Random.Range(-10, 10));
            GameObject passenger = Instantiate(passengerPrefab, startingPoint, Quaternion.identity);
            passenger.transform.SetParent(this.transform);
            passengers.Add(passenger);
        }
    }

    public void TrainArrived(Train train)
    {
        for (int i = 0; i < passengers.Count; i++)
        {
            GameObject passenger = passengers[i];
            PassengerNavmesh passengerScript = passenger.GetComponent<PassengerNavmesh>();
            train.BuildEntrancePath(passengerScript);
        }
    }
}
