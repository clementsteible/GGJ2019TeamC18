using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public float patrolTime = 10f;
    public float aggroRange = 0f;
    public Transform[] waypoints;

    private int index;
    public float speed, agentSpeed;
    private Transform fromage;
    private GameObject fromageBool;

    private Animator anim;
    private NavMeshAgent agent;

    private void Update()
    {
        if (fromageBool.GetComponent<FromageBool>().fromageExiste)
        {
            fromage = GameObject.FindGameObjectWithTag("Fromage").transform;
        }

    }

    private void Awake()
    {

        fromageBool = GameObject.Find("FromageExiste");

        waypoints[0] = GameObject.FindGameObjectWithTag("Maison").transform;
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

    void Tick()
    {
        agent.destination = waypoints[index].position;
        agent.speed = agentSpeed / 2;
        
        if (fromage != null && Vector2.Distance(transform.position, fromage.position) < aggroRange)
        {
            agent.destination = fromage.position;
            agent.speed = agentSpeed/2;
        }
        
    }
}
