using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Station> Stations = new List<Station>();
    public int totalPassengers;
    public Text passengers;
    public Train train;

    void Start()
    {
        try // when not running from main menu, generate 5 passengers (debug tool)
        {
            totalPassengers = MainMenu.instance.numOfPassengers;
        }catch(Exception)
        {
            totalPassengers = 6;
        }

        int passengersPerStation = totalPassengers / Stations.Count;
        for (int i = 0; i < Stations.Count-1; i++)
        {
            Stations[i].numOfPassengers = passengersPerStation;
        }
        Stations[Stations.Count-1].numOfPassengers = passengersPerStation + totalPassengers % Stations.Count;
        foreach (Station station in Stations)
        {
            station.SpawnPassengers();
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        passengers.text = "Passengers in train: " + train.Passengers.Count.ToString();
    }
}
