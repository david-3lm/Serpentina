using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA_Patrol : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints = null;
    private int idx;
    private NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        idx = 0;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        agent.SetDestination(waypoints[idx].position);
        Debug.Log(Vector3.Distance(transform.position, waypoints[idx].position));
        if (!(Vector3.Distance(transform.position, waypoints[idx].position) < .1f)) return;
        
        idx++;
        if (idx >= waypoints.Count)
            idx = 0;
    }
}