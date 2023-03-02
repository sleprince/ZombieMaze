using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CameraTrigger : MonoBehaviour
{
    //CinemachineVirtualCamera mainCam;
    [SerializeField] CinemachineVirtualCamera switchCam;
    [SerializeField] private bool exiting;
    public GameManager game;

    private void Start()
    {
        //mainCam = GameObject.Find("PlayerCam").GetComponent<CinemachineVirtualCamera>();

    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(name + " colliided with " + other.gameObject.name);
        //collided with

        Scene mummy = SceneManager.GetActiveScene();

        if (mummy.name == "Mummy")
            game.Win();


            if (!exiting)
        {
            switchCam.Priority = 11;
            exiting = true;
        }
        else
        {
            switchCam.Priority = 9;
            exiting = false;
        }

    }


}