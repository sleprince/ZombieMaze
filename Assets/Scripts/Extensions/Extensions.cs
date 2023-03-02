using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public static class Extensions //static as remains unchanged, functions used by other scripts, no need to set source.
{
	public static bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject(); //used to check if the mouse is over the UI that is the dialogue panel.

    }

    public static Item CopyItem(Item item) //for copying item from item database to inventory.
    {
        Item newItem = new Item(item.ItemId, item.ItemName, item.ItemDesc, item.ItemSprite, item.ItemCursor); //constuctor function is
        //in Item.cs
        //can only get and not modify any of these item properties which should stop other scripts changing/interfering with the values.

        return newItem; //return the new item that has been created.
    }

    public static void RunActions(Actions[] actions)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act();
        }
    }

    public static List<T> FindObjectsOfTypeAll<T>()
    {
        List<T> result = new List<T>();
        SceneManager.GetActiveScene().GetRootGameObjects().ToList().ForEach(g => result.AddRange(g.GetComponentsInChildren<T>()));

        return result;
    }

    public static void SaveItemsToId(this List<int> itemsId, List<Item> inventory)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (itemsId.Contains(inventory[i].ItemId))
                return;

            itemsId.Add(inventory[i].ItemId);
        }
    }

    public static void LoadIdToItems(this List<Item> inventory, ItemDatabase itemDatabase, List<int> itemsId)
    {
        for (int i = 0; i < itemsId.Count; i++)
        {
            Item item = CopyItem(itemDatabase.GetItem(itemsId[i]));
            inventory.Add(item);
        }
    }

}//class end.
