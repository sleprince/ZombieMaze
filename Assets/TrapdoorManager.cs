using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class TrapdoorManager : MonoBehaviour
{
    Vector3 moveDirection = Vector3.zero;
    public float gravity = 20.0f;


    // Start is called before the first frame update
    void Start()
    {

        


    }

    // Update is called once per frame
    void Update()
    {

        moveDirection.y -= gravity * Time.deltaTime;

    }

    private void OnTriggerStay(Collider other)
    {
       Debug.Log(other.gameObject.name + " collided with " + this.gameObject.name);

        if (other.tag == "Zombie"
            && this.GetComponent<MeshRenderer>().enabled == false)
        {
            
            //other.GetComponent<EnemyController>().enabled = true;

            other.GetComponent<NavMeshAgent>().enabled = false;
            other.enabled = false;


        }




    }



}
