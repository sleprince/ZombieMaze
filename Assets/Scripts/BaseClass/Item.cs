using UnityEngine;

[System.Serializable]
public class Item
{
    [SerializeField] int itemId;
    [SerializeField] string itemName;

    [TextArea(1, 3)]
    [SerializeField] string itemDescription;

    [SerializeField] Sprite itemSprite;
    [SerializeField] Texture2D itemCursor; //equivalent texture for cursor when using item.
    [SerializeField] int amount;

    //constructor, like a function that has the same name as the class. When you initialise a new Item you can specify all
    //these properties instead of all blank.
    public Item(int itemId, string name, string desc, Sprite sprite, Texture2D cursor)
    {
        this.itemId = itemId; //this. needed because it has the same name.
        itemName = name;
        itemDescription = desc;
        itemSprite = sprite;
        itemCursor = cursor;
    }

    public int ItemId { get { return itemId; } } //public getter, to get the private serializedfields defined above.
    //capital I to differentiate that it is a property and the lowercase i is a variable.
    public string ItemName { get { return itemName; } }
    public string ItemDesc { get { return itemDescription; } }
    public Sprite ItemSprite { get { return itemSprite; } }
    public Texture2D ItemCursor { get { return itemCursor; } }
    public int Amount { get { return amount; } }

    public void ModifyAmount(int value)
    {
        amount += value;
    }

}//class end
