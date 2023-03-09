using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{

    private GameObject trapdoor;
    private Material pedestal;

    // Start is called before the first frame update
    void Start()
    {

        trapdoor = transform.GetChild(0).gameObject;
        pedestal = GetComponent<MeshRenderer>().material;
        
    }

    // Update is called once per frame
    void Update()
    {


        
    }

    private void OnTriggerEnter(Collider other)
    {
        pedestal.color = Color.red;


        //if (Input.GetKey(KeyCode.E))
            StartCoroutine(TrapdoorOpen());
        
    }

    private void OnTriggerExit(Collider other)
    {
        pedestal.color = Color.white;
    }

    IEnumerator TrapdoorOpen()
    {
        trapdoor.SetActive(false);

        yield return new WaitForSeconds(3f);

        trapdoor.SetActive(true);
    }
}
