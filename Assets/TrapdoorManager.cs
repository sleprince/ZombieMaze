using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class TrapdoorManager : MonoBehaviour
{

    private MeshCollider trapdoorCollider;
    [SerializeField] private Transform destination;


    // Start is called before the first frame update
    void Start()
    {

        trapdoorCollider = GetComponent<MeshCollider>();


    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnTriggerStay(Collider other)
    {
       Debug.Log(other.gameObject.name + " collided with " + this.gameObject.name);

        if (other.tag == "Zombie"
            && trapdoorCollider.enabled == false)
        {
            
            //other.GetComponent<EnemyController>().enabled = true; //this worked but was inefficient

            other.GetComponent<NavMeshAgent>().enabled = false;
            other.enabled = false;


        }

        if (other.tag == "BigZombie")
        {
            //other.GetComponent<NavMeshAgent>().enabled = false; //no longer needed
            other.GetComponent<EnemyAI>().enabled = false;
            other.GetComponent<NavMeshAgent>().destination = destination.position;
            other.GetComponent<NavMeshAgent>().baseOffset = -1.44f; //move him into stuck in the ground






        }
    }

}
