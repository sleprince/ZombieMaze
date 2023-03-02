using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class MessageItemAction : Actions
{
    [TextArea(5, 3)] //makes the text input field in Editor be multiline and have word wrap.
    [SerializeField] List<string> message; //now our messages are an array of messages.
    [SerializeField] bool enableDialogue;
    [SerializeField] string yesText, noText;
    [SerializeField] List<Actions> yesActions, noActions;

    [SerializeField] ItemDatabase itemDatabase; //drag and drop item database object here in editor.
    private bool rightItem;
    [SerializeField] string requiredItem;
    [SerializeField] Item currentItem; //this solves the null exception reference bug.
    public Item CurrentItem { get { return currentItem; } }

    private InventoryItemUI inventoryUI;

    public ItemDatabase ItemDatabase { get { return itemDatabase; } }

    public void ChangeItem(Item item)
    {
        if (CurrentItem.ItemId == item.ItemId)
            return; //skip code below if the current item is already the correct item.

        if (itemDatabase != null)
            currentItem = Extensions.CopyItem(item);
    }

    public bool ItemMatch(string item)
    {
        bool val = false;
        val = (requiredItem == item);
        return val;
    }

    public override void Act()
    {
        inventoryUI = FindObjectOfType<InventoryItemUI>();

        Item currItem = InventoryItemUI.ChosenItem;

        if (currItem != null)
            rightItem = ItemMatch(currItem.ItemName);

        if (rightItem)
        {

            //Debug.Log(message);      

            //passing over all the serialized fields above.
            DialogueSystem.Instance.ShowMessages(message, enableDialogue, yesActions, noActions, yesText, noText);
            DialogueSystem.Instance.msgId = 0; //to stop the same message coming up again if you click the NPC again straight away. Doesn't work.

        }
    }
}