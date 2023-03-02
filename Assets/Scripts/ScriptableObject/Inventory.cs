using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Custom Data/Inventory Data")]
public class Inventory : ScriptableObject
{
    [SerializeField] ItemDatabase itemDatabase; //need the itemdatabase data to create each inventory entry.
    [SerializeField] List<Item> inventory = new List<Item>();

    public event System.Action<List<Item>> OnItemChange = delegate { }; //16:00, lesson 38.

    public List<Item> GetInventory { get { return inventory; } }

    public void AddItem(Item item)
    {
        inventory.Add(item);
        OnItemChange(inventory); //the public event defined above.
    }

    public ItemDatabase ItemDatabase { get { return itemDatabase; } } //public getter.

    public int CheckAmount(Item item)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].ItemId == item.ItemId)
            {
                
                    return 1;
                
            }
        }
        return 0;
    }

    public void ModifyItemAmount(Item item, int amount, bool use = false)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].ItemId == item.ItemId)
            {

                    inventory[i].ModifyAmount(use ? -amount : amount); //if true subtract amount, if false add.

                    if (inventory[i].Amount <= 0 && use)
                        inventory.RemoveAt(i);
                
                else
                {
                    inventory.RemoveAt(i);
                }
                
                OnItemChange(inventory); //the public event defined above.
                return;
            }
        }

        Item newItem = Extensions.CopyItem(item);
        newItem.ModifyAmount(amount);

        AddItem(newItem);
        OnItemChange(inventory); //the public event defined above.
    }

    public void UpdateInventory(List<int> itemsId)
    {
        inventory.Clear();
        inventory.LoadIdToItems(itemDatabase, itemsId);
        OnItemChange(inventory);
    }

    public void ClearInventory()
    {
        inventory.Clear();

    }


}//class end.
