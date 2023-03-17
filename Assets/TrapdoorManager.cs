using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class TrapdoorManager : MonoBehaviour
{

    private MeshRenderer trapdoorRenderer;
    [SerializeField] private Transform destination;


    // Start is called before the first frame update
    void Start()
    {

        trapdoorRenderer = GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnTriggerStay(Collider other)
    {
       Debug.Log(other.gameObject.name + " collided with " + this.gameObject.name);

        if (other.tag == "Zombie"
            && trapdoorRenderer.enabled == false)
        {
            
            //other.GetComponent<EnemyController>().enabled = true; //this worked but was inefficient

            other.GetComponent<NavMeshAgent>().enabled = false;
            other.GetComponent<Rigidbody>().isKinematic= false;
            other.enabled = false;


        }

        if (other.tag == "BigZombie" && trapdoorRenderer.enabled == false)
        {
            //other.GetComponent<NavMeshAgent>().enabled = false; //no longer needed
            other.GetComponent<EnemyAI>().enabled = false;
            other.GetComponent<NavMeshAgent>().destination = destination.position;
            other.GetComponent<NavMeshAgent>().baseOffset = -1.44f; //move him into stuck in the ground






        }
    }

}
