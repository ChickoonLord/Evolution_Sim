using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            agent.SetDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
