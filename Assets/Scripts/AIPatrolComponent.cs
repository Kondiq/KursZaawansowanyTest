﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPatrolComponent : MonoBehaviour
{
    private List<Vector3> navPointsList;
    private int index = 0;
    private NavMeshAgent agent;
    bool isBeginning = true;

    void Start()
    {
        enabled = false;
        navPointsList = new List<Vector3>();
        //wypelnienie listy punktami
        navPointsList.Add(new Vector3(10.0f,1.0f,10.0f));
        navPointsList.Add(new Vector3(-10.0f, 1.0f, 10.0f));
        navPointsList.Add(new Vector3(10.0f, 1.0f, -10.0f));
        navPointsList.Add(new Vector3(15.0f, 1.0f, 5.0f));

        agent = GetComponent<NavMeshAgent>();

        if (navPointsList.Count > 0)
            enabled = true;
    }

    void Update()
    {             
         GoToNextNavPoint();
    }

    //pobranie nastepnego punktu
    Vector3 GetNextNavPoint()
    {
        //pierwszy punkt, poczatek listy na starcie sciezki
        if (isBeginning)
        {
            isBeginning = false;
            return navPointsList[0];
        }
        //kolejny punkt
        if (index + 1 < navPointsList.Count)
        {
            index++;
            return navPointsList[index];
        }
        //jesli doszedl do ostatniego punktu listy
        index = 0;
        return navPointsList[0];
    }

    //ustawienie celu na kolejny punkt
    void GoToNextNavPoint()
    {
        //sprawdzenie czy agent nie oblicza nowej sciezki i dystansu od celu
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            //ustawienie celu na nastepny navpoint
            agent.destination = GetNextNavPoint();
    }

    public void AddNavPoint(Vector3 point)
    {
        navPointsList.Add(point);
        enabled = true;
    }

    public void RemoveNavPoint(int index)
    {
        if (index < navPointsList.Count)
            navPointsList.RemoveAt(index);
        if (navPointsList.Count < 1)
            enabled = false;
    }
}
