using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    enum Behaviours
    {
        PATROL,
        CHASE,
        INVESTIGATE
    };
    private Behaviours behaviour = Behaviours.PATROL;

    public Animator enemy_animator;

    private float reachDistance = 2.5f; // Larger value due to the test enemy being larger in scale then previous cube primitive enemy

    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float chaseWaitTime = 1f;
    public Transform[] patrolWaypoints;

    private int waypointIndex = 0;
    private float timer = 0.0f;
    private NavMeshAgent agent;

    public GameObject character;

    private Vector3 InvestigateSpot;
    public float InvestigateWait = 10f;

    public float sightDist = 35.0f;
    public float heightMultiplier = 1.36f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Investigate()
    {
        timer += Time.deltaTime;

        RaycastHit hit;

        Debug.DrawRay(character.transform.position + Vector3.up * heightMultiplier, transform.forward * sightDist, Color.green);
        Debug.DrawRay(character.transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized * sightDist, Color.green);
        Debug.DrawRay(character.transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized * sightDist, Color.green);

        if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, sightDist))
        {
            if(hit.collider.gameObject.tag == "Player")
            {
                enemy_animator.SetInteger("CurrentAnim", 2);

                behaviour = Behaviours.CHASE;
            }
        }
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                enemy_animator.SetInteger("CurrentAnim", 2);

                behaviour = Behaviours.CHASE;
            }
        }
        if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, sightDist))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                enemy_animator.SetInteger("CurrentAnim", 2);

                behaviour = Behaviours.CHASE;
            }
        }

        agent.SetDestination(this.transform.position);
        transform.LookAt(InvestigateSpot);

        if (timer >= InvestigateWait)
        {
            enemy_animator.SetInteger("CurrentAnim", 3);

            behaviour = Behaviours.PATROL;
            timer = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            enemy_animator.SetInteger("CurrentAnim", 1);

            behaviour = Behaviours.INVESTIGATE;
            InvestigateSpot = other.gameObject.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (behaviour == Behaviours.PATROL)
        {
            agent.speed = 2.5f;
            float distance = Vector3.Distance(patrolWaypoints[waypointIndex].position, transform.position);
            agent.SetDestination(patrolWaypoints[waypointIndex].position);

            if (distance <= reachDistance) waypointIndex++;
            if (waypointIndex >= patrolWaypoints.Length) waypointIndex = 0;
        }
        else if(behaviour == Behaviours.INVESTIGATE)
        {
            Investigate();
        }
        else if(behaviour == Behaviours.CHASE)
        {
            agent.speed = 3.5f * 2;
            agent.SetDestination(GameObject.Find("PlayerController").transform.position);
        }
    }
}
