using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavIAPoussin : MonoBehaviour
{
    bool autorisationImproveStun = true;

    private bool isDialogueEnabled;

    public GameObject prefabDialogue;

    public float patrolTime = 10f;
    public Transform[] waypoints;

    private int index;
    public float speed, agentSpeed;

    private Animator anim;
    private NavMeshAgent agent;

    private void Awake()
    {
        isDialogueEnabled = false;

        waypoints[0] = GameObject.FindGameObjectWithTag("Maison").transform;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agentSpeed = 0;
            agent.acceleration = 0;
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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            if (!isDialogueEnabled)
            {
                isDialogueEnabled = true;
                Instantiate(prefabDialogue, transform.position, Quaternion.identity);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (GameObject.FindGameObjectWithTag("DialoguePoussin")!=null)
        {
            Destroy(GameObject.FindGameObjectWithTag("DialoguePoussin"));
            isDialogueEnabled = false;
        }

    }

    private void OnTriggerStay(Collider other)
    {

        if (Input.GetButton("Use"))
        {
            Destroy(GameObject.FindGameObjectWithTag("DialoguePoussin"));

            agentSpeed = 200;
            agent.acceleration = 100;
            gameObject.GetComponent<BoxCollider>().size = new Vector3(1,1,1);
            gameObject.GetComponent<BoxCollider>().isTrigger = false;

            if (autorisationImproveStun)
            {
                GameObject.FindGameObjectWithTag("TempsStun").GetComponent<StunTime>().tempsStunEnSecondes *= 1.5f;
                autorisationImproveStun = false;
            }
        }
    }
}
