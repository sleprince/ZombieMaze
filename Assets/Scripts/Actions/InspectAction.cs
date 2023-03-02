using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class InspectAction : Actions
{
    [TextArea(5, 3)] //makes the text input field in Editor be multiline and have word wrap.
    [SerializeField] List<string> message; //now our messages are an array of messages.
    

    public override void Act()
    {
        //Debug.Log(message);      
        
        //passing over all the serialized fields above.
        DialogueSystem.Instance.InspectMessage(message);


    }
}