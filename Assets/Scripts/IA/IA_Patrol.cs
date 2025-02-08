using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Random = UnityEngine.Random;

public class IA_Patrol : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints = null;
    private int idx;
    private NavMeshAgent agent;
    
    [Header("Vision")]
    [SerializeField] private float visionRadius = 10f;
    [SerializeField] private float visionAngle = 45f;
    [SerializeField] private Transform player;
    
    [Header("Layers")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask obstacleLayer;

    [Header("Speed")]
    [SerializeField] private float patrolSpeed = 5f;
    [SerializeField] private float followSpeed = 10f;
    [SerializeField] private float delay = 1f;
    
    public event Action OnPlayerDetected;
    public event Action OnPlayerLost;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        idx = 0;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, visionRadius, playerLayer);
        agent.SetDestination(waypoints[idx].position);
        if (targetsInViewRadius.Length > 0)
        {
            Transform target = targetsInViewRadius[0].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
    
            if (Vector3.Angle(transform.forward, dirToTarget) < visionAngle)
            {
                // Verificar si hay un obstáculo en medio
                if (!Physics.Raycast(transform.position, dirToTarget, visionRadius, obstacleLayer))
                {
                    // Se detecta al jugador
                    agent.speed = followSpeed;
                    agent.SetDestination(target.position);
                    OnPlayerDetected?.Invoke();
                    UIManager.Instance.BroadcastMsg("Vaya disfraz de gato más guapo, a ver que me acerque a verlo");
                }
            }
            else
            {
                OnPlayerLost?.Invoke();
                UIManager.Instance.BroadcastMsg("");
            }
        }
        else
            agent.speed = patrolSpeed;

        if (!(Vector3.Distance(transform.position, waypoints[idx].position) < .1f)) return;
        delay -= Time.deltaTime;
        if (delay > 0) return;
        delay = Random.Range(.5f, 1.5f);
        idx++;
        if (idx >= waypoints.Count)
            idx = 0;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);

        Vector3 leftBoundary = Quaternion.Euler(0, -visionAngle, 0) * transform.forward * visionRadius;
        Vector3 rightBoundary = Quaternion.Euler(0, visionAngle, 0) * transform.forward * visionRadius;

        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }

}