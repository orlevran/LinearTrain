using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            AtStation = true;
            CurrentSpeed = 0;
            UnloadPassengers();
            Stations[StationIndex].TrainArrived(this);
            //PassangerController.EnableOrDisableAgent(true);
            //Passanger.isWalking = true;
            //AnimationActivator.OutSideObjectActivator = true;
            StartCoroutine(WaitForPassengers());
            IEnumerator WaitForPassengers()
            {
                yield return new WaitForSeconds(2);
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
        //Debug.Log(Engine.transform.position);
        Engine.transform.position += engineOffset;
        //Debug.Log(Engine.transform.position);
        Engine.transform.LookAt(Stations[StationIndex].EndPoint);
    }
    
    void UnloadPassengers()
    {
        for (int i = 1; i < Cars.Count; i++)
        {
            WagonManager wagon = Cars[i].GetComponent<WagonManager>();
            wagon.UnloadWagon();
        }
        foreach (GameObject passenger in Passengers)
        {
            Passanger passengerScript = passenger.GetComponent<Passanger>();
            //passengerScript.Walk(Stations[StationIndex].EndPoint.transform);
            Vector3 destination = Stations[StationIndex].StationCenter.transform.position + new Vector3(Random.Range(-20,-20), 0, Random.Range(-10,10));
            passengerScript.transform.position = destination;
            Stations[StationIndex].passengers.Add(passenger);
            passenger.transform.SetParent(Stations[StationIndex].transform);
        }
        Passengers.Clear();
    }

    // search for an empty standing point in the wagon. return the station point if the train is full
    public Transform GetStandingPoint()
    {
        Debug.Log("check available wagon");
        for (int i = 1; i < Cars.Count; i++)
        {
            Debug.Log("wagon " + i);
            WagonManager wagon = Cars[i].GetComponent<WagonManager>();
            if (wagon.checkAvailability())
            {
                Transform standingPoint = wagon.GetStandingPoint();
                wagon.FillWagon();
                return standingPoint;
            }
        }
        return Stations[StationIndex].transform;
    }

}
