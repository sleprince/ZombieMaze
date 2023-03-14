using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{

    private GameObject trapdoor;
    private Material pedestal;

    private MeshRenderer trapdoorMesh;
    private MeshCollider trapdoorCollider;

    // Start is called before the first frame update
    void Start()
    {

        trapdoor = transform.GetChild(0).gameObject;
        pedestal = GetComponent<MeshRenderer>().material;
        trapdoorMesh = trapdoor.GetComponent<MeshRenderer>();
        trapdoorCollider = trapdoor.GetComponent<MeshCollider>();


    }

    // Update is called once per frame
    void Update()
    {


        
    }

    private void OnTriggerEnter(Collider other)
    {
        pedestal.color = Color.red; //to show visually that the switch has activated


        //if (Input.GetKey(KeyCode.E))
            StartCoroutine(TrapdoorOpen(other));
        
    }

    private void OnTriggerExit(Collider other)
    {
        pedestal.color = Color.white;
    }

    IEnumerator TrapdoorOpen(Collider other)
    {

        trapdoorMesh.enabled = false;

        //if (other.tag == "Player")
            trapdoorCollider.enabled= false; //so that player can fall through too

        yield return new WaitForSeconds(3f);

        trapdoorMesh.enabled = true;
        trapdoorCollider.enabled = true;
    }
}
