using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Train : MonoBehaviour
{
    public static Train instance;

    public GameObject Engine;
    public List<GameObject> Cars;
    public List<Station> Stations;
    public List<GameObject> Passengers;

    public bool AtStation = false;
    private float TrainLength = 0f;
    private float TrainTopSpeed = 400.0f;
    private float TrainMinSpeed = 10f;
    private float AccelerationSpeed = 0.3f;

    private float DistanceSlowdown = 500f;
    private float CurrentSpeed = 0;
    private int StationIndex = 0;

    private int status = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        // calculates the train length to reposition the engine
        foreach (GameObject car in Cars)
        {
            TrainLength += car.GetComponent<Collider>().bounds.size.x; // remember to add a collider to new cars
            car.GetComponent<BoxCollider>().isTrigger = false;
            car.GetComponent<BoxCollider>().enabled = false;
        }
        Passengers = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (status)
        {
            case 0:
                Accelerate();
                break;
            case 1:
                Drive();
                break;
            case 2:
                SlowDown();
                break;
            case 3:
                AriveAtStation();
                break;
            default:
                break;
        }
    }

    void Accelerate()
    {
        MoveTrain();
        CurrentSpeed += AccelerationSpeed;
        if (CurrentSpeed > TrainTopSpeed) // Make sure the train doesn't pass the top speed
        {
            CurrentSpeed = TrainTopSpeed;
            status = 1;
        }
    }

    void Drive()
    {
        MoveTrain();
        if (Vector3.Distance(transform.position, Stations[StationIndex].EndPoint.position) < DistanceSlowdown)
        {
            status = 2;
        }
    }
    void SlowDown()
    {
        MoveTrain();
        CurrentSpeed -= AccelerationSpeed * 1.5f;
        if (CurrentSpeed < TrainMinSpeed)
            CurrentSpeed = TrainMinSpeed;
        if (Vector3.Distance(transform.position, Stations[StationIndex].EndPoint.position) < 0.001f)
        {
            status = 3;
        }
    }
    void AriveAtStation()
    {
        if (!AtStation)
        {
            for (int i = 1; i < Cars.Count; i++)
            {
                AnimationActivator animation = Cars[i].GetComponent<AnimationActivator>();
                animation.ForceOpenDoors();
            }
            AtStation = true;
            CurrentSpeed = 0;
            StartCoroutine(WaitForPassengers());
            IEnumerator WaitForPassengers()
            {
                yield return new WaitForSeconds(2.5f); // wait until the doors open
                UnloadPassengers();
                yield return new WaitForSeconds(2.5f); // wait until all passengers get OFF the train
                Stations[StationIndex].TrainArrived(this);
                yield return new WaitForSeconds(8); // wait until all passengers get ON train
                for (int i = 1; i < Cars.Count; i++)
                {
                    AnimationActivator animation = Cars[i].GetComponent<AnimationActivator>();
                    animation.ForceCloseDoors();
                }
                yield return new WaitForSeconds(2.5f); // wait until the doors close
                ManagePassengers();
                NextStation();
                status = 0;
                AtStation = false;
            }
        }
    }

    void MoveTrain()
    {
        float Step = CurrentSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Stations[StationIndex].EndPoint.position, Step);
    }

    void NextStation()
    {
        if (StationIndex < Stations.Count - 1)
        {
            StationIndex++;
        }
        else
            StationIndex = 0;
        Vector3 engineOffset;
        if (StationIndex == 0)
            engineOffset = new Vector3(-TrainLength, 0, +6.856f);
        else
            engineOffset = new Vector3(TrainLength, 0, -6.856f);
        Engine.transform.position += engineOffset;
        Engine.transform.LookAt(Stations[StationIndex].EndPoint);
    }
    
    void UnloadPassengers()
    {
        foreach (GameObject passenger in Passengers)
        {
            PassengerNavmesh passengerAgent = passenger.GetComponent<PassengerNavmesh>();
            //navmesh.path.Add(navmesh.wagon.Center);
            //navmesh.path.Add(navmesh.wagon.Entrance);
            Transform dest = Stations[StationIndex].WaitingPoints[Random.Range(0, Stations[StationIndex].WaitingPoints.Count-1)];
            passengerAgent.path.Add(dest);
            passengerAgent.wagon = null;
        }
        //Passengers.Clear();
    }
    // used AFTER new passengers get no train. if not, the passenger will get off the train
    // and then try to aboard again.
    void ManagePassengers() 
    {
        List<GameObject> temp = new List<GameObject>();
        // from train to station
        foreach (GameObject passenger in Passengers)
        {
            temp.Add(passenger);
            passenger.transform.SetParent(Stations[StationIndex].transform);
        }
        Passengers.Clear();
        // from station to train
        for (int i = 0; i < Stations[StationIndex].passengers.Count; i++)
        {
            GameObject passenger = Stations[StationIndex].passengers[i];
            PassengerNavmesh passengerScript = passenger.GetComponent<PassengerNavmesh>();
            Passengers.Add(passenger);
            passenger.transform.SetParent(passengerScript.wagon.transform);
        }

        Stations[StationIndex].passengers = temp;
    }
    
    //build a path to a passengers that want to enter the train
    public void BuildEntrancePath(PassengerNavmesh passenger)
    {
        WagonManager wagon = Cars[(int)Random.Range(1, Cars.Count)].GetComponent<WagonManager>();
        //WagonManager wagon = Cars[1].GetComponent<WagonManager>();
        List<Transform> list = new List<Transform>();
        //list.Add(wagon.Entrance);
        //list.Add(wagon.Center);
        Transform dest = wagon.GetStandingPoint();
        list.Add(dest);
        passenger.path = list;
        passenger.wagon = wagon;
        passenger.StandingPoint = dest;
    }


}
