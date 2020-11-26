using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public GameObject Engine;
    public List<GameObject> Cars;
    public List<Transform> Stations;

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
        // calculates the train length to reposition the engine
        foreach (GameObject car in Cars)
        {
            TrainLength += car.GetComponent<Collider>().bounds.size.x; // remember to add a collider to new cars
        }
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
        if (Vector3.Distance(transform.position, Stations[StationIndex].position) < DistanceSlowdown)
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
        if (Vector3.Distance(transform.position, Stations[StationIndex].position) < 0.001f)
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
            StartCoroutine(WaitForPassengers());
            IEnumerator WaitForPassengers()
            {
                yield return new WaitForSeconds(5);
                NextStation();
                status = 0;
                AtStation = false;
            }
        }
    }

    void MoveTrain()
    {
        float Step = CurrentSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Stations[StationIndex].position, Step);
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
        Engine.transform.LookAt(Stations[StationIndex]);
    }

}
