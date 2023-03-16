using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationHandler : MonoBehaviour
{
    [SerializeField] private GameObject player; //only needed if we only want to teleport the player, might be funny if npcs can teleport too
    [SerializeField] private Transform teleportExit;

     // Start is called before the first frame update
     void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.name + " collided with " + this.gameObject.name);


        other.GetComponent<CharacterController>().Move(teleportExit.position); //do the teleportation
    }


}
