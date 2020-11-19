using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public GameObject Engine;
    public List<GameObject> Cars;
    public List<Transform> Stations;

    private bool isAccelerating = true;

    private float TrainLength = 0f;
    private float TrainTopSpeed =1000.0f;
    private float AccelerationSpeed = 0.05f;

    private float CurrentSpeed = 0;
    private int StationIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject car in Cars)
        {

            TrainLength += car.GetComponent<Collider>().bounds.size.x; // remember to add a collider to new cars
        }
        Debug.Log(TrainLength);
    }

    // Update is called once per frame
    void Update()
    {
        ManageSpeed();
        float Step = CurrentSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Stations[StationIndex].position, Step);
        if (Vector3.Distance(transform.position, Stations[StationIndex].position) < 0.001f)
        {
            isAccelerating = false;
            AllAboard();
            NextStation();
        }
    }

    void AllAboard()
    {

    }

    void NextStation()
    {
        if (StationIndex < Stations.Count-1)
        {
            StationIndex++;
        }
        else
            StationIndex = 0;
        isAccelerating = true;
        Vector3 engineOffset;
        if (StationIndex == 0)
            engineOffset = new Vector3(-TrainLength, 0, +4.456f);
        else
            engineOffset = new Vector3(TrainLength, 0, -4.456f);
        //Debug.Log(Engine.transform.position);
        Engine.transform.position += engineOffset;
        //Debug.Log(Engine.transform.position);
        Engine.transform.LookAt(Stations[StationIndex]);
    }

    void ManageSpeed()
    {
        if (isAccelerating)
        {
            CurrentSpeed += AccelerationSpeed; //Accelerate the train
        }
        else if (CurrentSpeed > 0)
        {
            CurrentSpeed -= AccelerationSpeed*10; //Decelerate faster
        }

        if (CurrentSpeed > TrainTopSpeed) // Make sure the train doesn't pass the top speed
        {
            CurrentSpeed = TrainTopSpeed;
        }
    }

}
