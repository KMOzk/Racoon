using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

[SelectionBase]
public class NPCMovement : MonoBehaviour
{
    /// <summary>
    /// The Nevmash agent we want to control.
    /// </summary>
    private NavMeshAgent npcAgent;

    #region Waypoints

    [Header("Way-points.")]

    private int waypointIndex;

    /// <summary> 
    /// Before going to the next waypoint we can have it wait a period of time before going on.
    /// </summary>
    [SerializeField] private float       waitTimer;

    /// <summary>
    /// For when you want the npc to move in a random direction.
    /// </summary>
    [SerializeField] private bool        walkingPointRandom;
    
    /// <summary>
    /// We want to have a distance a npc can go to the next checkpoint.
    /// </summary>
    [SerializeField] private float       distanceForNextCheckpoint;
    [SerializeField] private Transform[] waypoints = new Transform[0];
   
    #endregion Waypoints

    private void Start()
    {
        npcAgent = GetComponent<NavMeshAgent>();
        npcAgent.SetDestination(waypoints[0].position); // The npc goes to the first waypoints in the list.
    }

    private void Update()
    {
        waitTimer -= Time.deltaTime;

        if (waitTimer <= 0f) npcAgent.speed = 3f;

        Patrol();

    }

    public void Patrol()
    {
        if (npcAgent.remainingDistance < distanceForNextCheckpoint)
        {
            if (walkingPointRandom) 
            {
                waypointIndex = Random.Range(0, waypoints.Length); 
            }
            else waypointIndex++;

            npcAgent.speed = 0f;
            waitTimer = 3f;

            if (waypointIndex >= waypoints.Length)
                waypointIndex = 0;

            npcAgent.SetDestination(waypoints[waypointIndex].position);
        }
    }
}