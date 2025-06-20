using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombiePatrolingState : StateMachineBehaviour
{
    float timer;
    public float patrolingTime = 0f;

    Transform player;
    NavMeshAgent agent;


    public float detectionArea = 20f;
    public float patrolingSpeed = 2f;
    
    List<Transform> waypointsList = new List<Transform>();

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // --- Initializations --- //
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();


        agent.speed = patrolingSpeed;
        timer = 0f;

        // --- Get all waypoints and Move to first Waypont --- //

        GameObject waypointsCluster = GameObject.FindGameObjectWithTag("Waypoints");
        foreach (Transform t in waypointsCluster.transform)
        {
            waypointsList.Add(t);

        }
        Vector3 nextPosition = waypointsList[Random.Range(0, waypointsList.Count)].position;
        agent.SetDestination(nextPosition);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // --- ZombiePatrolingSound --- //
        if (!SoundManager.Instance.zombieChannel.isPlaying)
        {
            SoundManager.Instance.zombieChannel.clip = SoundManager.Instance.zombieWalking;
            SoundManager.Instance.zombieChannel.PlayDelayed(1f);
        }

        // --- If agent arrived at waypoint, move to next waypoint --- //
        if (agent.remainingDistance == agent.stoppingDistance)
        {
            agent.SetDestination(waypointsList[Random.Range(0, waypointsList.Count)].position);
        }

        // --- Transition to Idle State --- //

        timer += Time.deltaTime;
        if (timer > patrolingTime)
        {
            animator.SetBool("isPatroling", true);
        }

        // --- Transition to Chasing State --- //

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        



        if (distanceFromPlayer < detectionArea)
        {
            animator.SetBool("isChasing", true);
        }

    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Stop the agent
        SoundManager.Instance.zombieChannel.Stop();

    }


    
}
