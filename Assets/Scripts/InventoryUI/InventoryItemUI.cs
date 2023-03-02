using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    private Item thisItem;
    private Button button;
    private InventorySystemUI invSystem; //parent class that handles redrawing changes in items held

    private static Item chosenItem; //has to be static for ItemAction to access it.

    //[SerializeField] private Texture2D itemCursor; //the cursor that the button will change the mouse to.
    [SerializeField] private Image itemImage; //the image that will be attached to the button
    PlayerScript _pScript;

    InventorySystemUI invSystemUI;

    
    private TMPro.TextMeshProUGUI descriptionText;
    
    public static Item ChosenItem { get { return chosenItem; } }

    public void Init(Item item, InventorySystemUI invSystem)
    {
        _pScript = FindObjectOfType<PlayerScript>();
        invSystemUI = invSystem;


        thisItem = item;
        this.invSystem = invSystem; //have to use this. because the internal variable has the same name

        button = GetComponent<Button>();

        //descriptionText = descriptionPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>();

        itemImage.sprite = item.ItemSprite;

        //invSystemUI.descriptionPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = item.ItemDesc; 
        
        
            //add a listener for each button
            button.onClick.AddListener(() => OnClick(button, item));
      
    }
    

    void OnClick(Button btn, Item itm)
    {
        invSystemUI.transform.Find("DescriptionPanel").gameObject.SetActive(false);
        
        if (_pScript.I == 0)
        {
            invSystemUI.transform.Find("DescriptionPanel").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = itm.ItemDesc;
            invSystemUI.transform.Find("DescriptionPanel").gameObject.SetActive(true);
            
        }
        else
        {
            Cursor.SetCursor(itm.ItemCursor,_pScript.hotSpot, _pScript.cursorMode); //set the cursor to be the item image.
            GameObject.Find("Inventory UI Panel").gameObject.SetActive(!gameObject.activeInHierarchy); //turn off UI panel.
            chosenItem = itm;
            //set cursor to item mode integer
            _pScript.I = 2;
            
        }

       
       
    }

    void Update()
    {

        //DescriptionShow();


        

    }


    private void OnMouseOver(Item item)
    {
        invSystemUI.descriptionPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = item.ItemDesc;
        invSystemUI.descriptionPanel.SetActive(true);
    }

    private void OnMouseExit()
    {
        invSystemUI.descriptionPanel.SetActive(false);
    }

}//class end
