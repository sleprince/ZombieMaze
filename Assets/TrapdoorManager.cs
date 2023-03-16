using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class TrapdoorManager : MonoBehaviour
{
    //Vector3 moveDirection = Vector3.zero;
    //public float gravity = 20.0f;

    private MeshCollider trapdoorCollider;


    // Start is called before the first frame update
    void Start()
    {

        trapdoorCollider = GetComponent<MeshCollider>();


    }

    // Update is called once per frame
    void Update()
    {

        //moveDirection.y -= gravity * Time.deltaTime;

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
            other.GetComponent<NavMeshAgent>().enabled = false;
            other.GetComponent<EnemyAI>().enabled = false;
            //other.transform.Translate(0, -1, 0); //move him into stuck in the ground

        }




    }



}
