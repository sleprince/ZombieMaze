using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; private set; } //awesome line of code that makes it unnecesary to use getcomponent etc to use things
                                                                //from this script in another script.

    [SerializeField] TMPro.TextMeshProUGUI messageText, yesText, noText; //note what's needed to get TMP text working for future reference.
    public GameObject panel; //the dialogue panel we made earlier.
    [SerializeField] Button yesButton, noButton; //basic options for player. Right and no
    //answers, going to pass these to ShowMessage in MessageAction.

    private List<string> currentMessages = new List<string>();
    public int msgId = 0;

    //public int MsgID { get { return msgId; } } //public getter.

    //used to stop player being able to walk around while talking.
    public bool conversing = false;



    private void Awake()
    {
        Instance = this; //for the public staticness of the script.


    }

    // Use this for initialization
    void Start()
    {
        panel.SetActive(false); //so that dialogue panel is not visible to begin with.




    }

    public void ShowMessages(List<string> messages, bool dialogue, List<Actions> yesActions = null, List<Actions> noActions = null, string yes = "Yes", string no = "No")
    // = null is so that when there are no yes,no actions in the dialog we don't have to send anything over.
    {


        msgId = 0;



        yesButton.transform.parent.gameObject.SetActive(false);

        currentMessages = messages; //we will pass our messages into here from our interactable.

        panel.SetActive(true);

        conversing = true; //bool used so that the player will not be able to move while conversing.

        if (dialogue) //if the message includes dialog options.
        {
            yesText.text = yes;
            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(delegate
            {
                panel.SetActive(false);

                if (yesActions != null)
                    AssignActionstoButtons(yesActions);
            });

            noText.text = no;
            noButton.onClick.RemoveAllListeners();
            noButton.onClick.AddListener(delegate
            {
                panel.SetActive(false);


                if (noActions != null)
                    AssignActionstoButtons(noActions);
            });
        }

        StartCoroutine(ShowMultipleMessages(dialogue));


    }

    public void InspectMessage(List<string> messages)
    {

        msgId = 0;



        yesButton.transform.parent.gameObject.SetActive(false);

        currentMessages = messages; //we will pass our messages into here from our interactable.

        panel.SetActive(true);

        conversing = true; //bool used so that the player will not be able to move while conversing.

        bool dialogue = false;

        StartCoroutine(ShowMultipleMessages(dialogue));




    }
    IEnumerator ShowMultipleMessages(bool useDialogue)
    {
        messageText.text = currentMessages[msgId]; //changing the TMP text to be the current message.

        if (msgId == currentMessages.Count - 1) //if it was the last message last time, reset message ID and don't do anything else.
        {
            msgId = 0;
            //yield return null;
        }

        while (msgId < currentMessages.Count)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) //&& Extensions.IsMouseOverUI())
            {


                msgId++;

                if (msgId < currentMessages.Count)
                    messageText.text = currentMessages[msgId]; //message text updates to next message.

                if (useDialogue && msgId == currentMessages.Count - 1) //click to show the options.
                { //when we get to our last message show the buttons.
                    yesButton.transform.parent.gameObject.SetActive(true); //using parent because then we can just 
                                                                           //hide entire dialogue panel when we setactive to false.
                                                                           //msgId = 0;
                }

                //if (!useDialogue && msgId == currentMessages.Count - 1 || useDialogue && msgId == currentMessages.Count - 1)
                //move player back to solve bug.




            }

            yield return null;
        }


        if (!useDialogue)
            panel.SetActive(false);

        conversing = false;


    }

    void AssignActionstoButtons(List<Actions> actions)
    {
        List<Actions> localActions = actions; //think this is unneeded?

        for (int i = 0; i < localActions.Count; i++)
        {
            localActions[i].Act();
        }
    }

    public void HideDialog() //for if you walk off during the dialogue.
    {
        conversing = false;
        panel.SetActive(false);
    }

}//class end.