using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathController : MonoBehaviour
{

    [SerializeField] private GameManager game;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.gameObject.name + " collided with " + this.gameObject.name);

        if (other.tag == "Player")
            game.Lose();



    }
}
