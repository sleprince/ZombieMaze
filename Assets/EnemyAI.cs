using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    private enum State {
        Roaming,
        ChaseTarget
    }

    private State state;
    private NavMeshAgent agent;
    public float speed = 1.0f;
    private Vector3 destination;
    [SerializeField] private GameManager game;
    [SerializeField] private GameObject player;


    private void Awake() {

        state = State.Roaming; //default state is roaming
        agent = GetComponent<NavMeshAgent>();
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0); //point in a random direction at the start
        destination = GetRandomPositionOnNavMesh(); //function to get a random destination within the NavMesh bounds
        agent.SetDestination(destination); //set the NavMesh agent's destination to this

    }

    private void Update() {

        //switch statement that uses the state variable, default is roaming
        switch (state) {
        default:
        case State.Roaming:         

                if (agent.remainingDistance < 0.5f) //when destination reached, generate a new random destination
                {
                    destination = GetRandomPositionOnNavMesh();
                    agent.SetDestination(destination);
                }

                FindTarget(); //when roaming, constantly calling the FindTarget method
                break;
        case State.ChaseTarget:
            agent.destination = player.transform.position; //destinated becomes player position


                float catchRange = 2f;
            if (Vector3.Distance(transform.position, player.transform.position) < catchRange) {
                    //if player is within catch range they lose the game and the scene reloads

                    //Debug.Log("You got caught.");
                    game.Lose();

            }

            float stopChaseDistance = 7f;
            if (Vector3.Distance(transform.position, player.transform.position) > stopChaseDistance) {
                // too far, stop chasing
                state = State.Roaming;
            }
            break;
        }
    }


    private Vector3 GetRandomPositionOnNavMesh()
    {
        //generates a random position within a sphere of radius 20 units around the origin
        Vector3 randomPoint = Random.insideUnitSphere * 20.0f;

        //adds the position vector of the object that this script is attached to, so that the random position is centered around
        //the object's position rather than being cenetered around the scene position (usually 0,0,0)
        randomPoint += transform.position;

        // Declare a NavMeshHit variable to hold information about the closest point on the NavMesh to the random position
        NavMeshHit navMeshHit;

        // Use NavMesh.SamplePosition to find the closest point on the NavMesh to the random position, within a radius of 20
        // units and in all NavMesh areas, output the information about this point to navMeshHit
        NavMesh.SamplePosition(randomPoint, out navMeshHit, 20.0f, NavMesh.AllAreas);

        // Return the position of the closest point on the NavMesh to the random position
        return navMeshHit.position;
    }

    private void FindTarget() {
        float targetRange = 15f;


        if (Vector3.Distance(transform.position, player.transform.position) < targetRange) {
            // if player is within target range, change to the chasing state
            state = State.ChaseTarget;
        }
    }

}
