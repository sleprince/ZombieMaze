using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class MessageAction : Actions
{
    [TextArea(5, 3)] //makes the text input field in Editor be multiline and have word wrap.
    [SerializeField] List<string> message; //now our messages are an array of messages.
    [SerializeField] bool enableDialogue;
    [SerializeField] string yesText, noText;
    [SerializeField] List<Actions> yesActions, noActions;



    private void Start()
    {

    }

    public override void Act()
    {
        //Debug.Log(message);      

        //passing over all the serialized fields above.
        DialogueSystem.Instance.ShowMessages(message, enableDialogue, yesActions, noActions, yesText, noText);
        DialogueSystem.Instance.msgId = 0; //to stop the same message coming up again if you click the NPC again straight away. Doesn't work.


    }
}