using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class NPCMovement : MonoBehaviour
{
    private NavMeshAgent npcAgent;

    #region Waypoints

    [Header("Way-points.")]
    private float waitTimer;
    private int waypointIndex;
    [SerializeField] private bool walkingPointRandom;
    [SerializeField] private float distanceForNextCheckpoint;
    [SerializeField] private Transform[] waypoints = new Transform[0];
    #endregion Waypoints

    void Start()
    {
        npcAgent = GetComponent<NavMeshAgent>();
        npcAgent.SetDestination(waypoints[0].position);
    }

    void Update()
    {
        waitTimer -= Time.deltaTime;

        if (waitTimer <= 0f) npcAgent.speed = 3f;

        Patrol();

    }
    public void Patrol()
    {
        if (npcAgent.remainingDistance < distanceForNextCheckpoint)
        {
            if (walkingPointRandom) { waypointIndex = Random.Range(0, waypoints.Length); }
            else waypointIndex++;

            npcAgent.speed = 0f;
            waitTimer = 3f;

            if (waypointIndex >= waypoints.Length)
                waypointIndex = 0;

            npcAgent.SetDestination(waypoints[waypointIndex].position);
        }
    }
}