using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VacheIA : MonoBehaviour
{
    public float patrolTime = 10f;
    public float aggroRange = 0f;
    public GameObject fromagerie;
    public Transform[] waypoints = new Transform[1];
    private Transform origin;

    private int index;
    public float speed, agentSpeed;

    private Animator anim;
    private NavMeshAgent agent;
    

    private void Awake()
    {
        origin = transform;
        GoHome();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agentSpeed = agent.speed;
        }
        
        index = Random.Range(0, waypoints.Length);
        InvokeRepeating("Tick", 0, 0.5f);

        if (waypoints.Length > 0)
        {
            InvokeRepeating("Patrol", 0, patrolTime); 
        }
    }

    void Patrol()
    {
        index = index == waypoints.Length - 1 ? 0 : index + 1;
    }

    public void GoToFrom()
    {
        waypoints[0] = fromagerie.transform;
    }

    public void GoHome()
    {
        waypoints[0] = origin;
    }

    void Tick()
    {
        agent.destination = waypoints[index].position;
        agent.speed = agentSpeed / 2;
    }
}
