using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystemUI : MonoBehaviour
{
    [SerializeField] private Transform itemsParents; //for the item layout.
    [SerializeField] private InventoryItemUI itemUIprefabs; //prefab of 1 button with 1 item in the grid on the inventory UI panel. Child of item layout.
    [SerializeField] private Inventory playerInventory;
    
    public GameObject descriptionPanel;

    private List<InventoryItemUI> itemUICollection = new List<InventoryItemUI>(); //all itemUI objects that we instantiate
                                                                                  //at runtime will be stored in this list.
    // Start is called before the first frame update.
    void Start()
    {
        playerInventory.OnItemChange += Redraw; //redraw whenever item added.
        
        Init(playerInventory.GetInventory); //passing in list of items from our Inventory script, using public getter.
        gameObject.SetActive(false); //to make the inventory invisible when the game starts, as this script will be attached to the inventory UI.
        
    }

    private void OnDestroy()
    {
        playerInventory.OnItemChange -= Redraw; //redraw whenever item taken away
    }

    void Init(List<Item> items) //initialize
    {
        for (int i = 0; i < items.Count; i++)
        {
            AddItemUI(items[i]); //loops through and makes new item UI for each item in player inventory.
        }
        
    }

    void AddItemUI(Item item)
    {
        InventoryItemUI tempItem = Instantiate(itemUIprefabs, itemsParents); //instantiate itemUIprefabs but keep the InventoryItemUI component reference.
        tempItem.Init(item, this); //access the public Init method on InventoryItemUI, passing in item and InventorySystemUI.
        itemUICollection.Add(tempItem); //add this item to our list of items instantiated at runtime.
    }

    void Redraw(List<Item> items)
    {
        for (int i = 0; i < itemUICollection.Count; i++)
        {
            Destroy(itemUICollection[i].gameObject); //destroy all items in UI.
        }

        itemUICollection.Clear(); //destroy with fire, delete any traces.
        
        Init(items); //redo init to add the itemUIs again.
    }

    public void ShowInventory()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy); //inventory panel in UI, if on switches it off, if off switches on.
        //toggle effect.
        
    }

    
} //class end.
