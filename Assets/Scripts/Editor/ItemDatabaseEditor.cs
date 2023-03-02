using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemDatabase))] //derives from itemdatabase scriptable object class.
public class ItemDatabaseEditor : Editor
{
    ItemDatabase source;
    SerializedProperty s_items, s_itemsName;

    private void OnEnable()
    {
        source = (ItemDatabase)target; //get item database.
        s_items = serializedObject.FindProperty("items");
        s_itemsName = serializedObject.FindProperty("itemsNames");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update(); //so that the new item appears immediately in ispector when added on the next frame.

        //base.OnInspectorGUI();
        if (GUILayout.Button("Add Item")) //adds a button to add item, if clicked a new item is added with an ID number and all the empty fields.
        {
            Item newItem = new Item(s_items.arraySize, "", "", null, null); //item id is arraysize because it will start at 0 and increase as items added.
            source.AddItem(newItem); //adds a blank item above, using contructor parameters.
        }

        for (int i = 0; i < s_items.arraySize; i++)
        {
            //draw the item entry
            DrawItemEntry(s_items.GetArrayElementAtIndex(i));
        }

        if (GUI.changed)
            ReCalculateID(); //without this the changes are not reflected in the inventory.

        serializedObject.ApplyModifiedProperties(); //so that the changes made in the inspector get saved.
    }

    void DrawItemEntry(SerializedProperty item)
    {
        GUILayout.BeginVertical("box");

        GUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Item Id:" + item.FindPropertyRelative("itemId").intValue, GUILayout.Width(75f)); //statuc field, cannot change, for ID number.
        //extracting itemId integer value. Set as a very narrow UI field. Width is no. of pixels.

        EditorGUILayout.PropertyField(item.FindPropertyRelative("itemName")); //editable field for item name.

        if (GUILayout.Button("X", GUILayout.Width(20f))) //small width, delete button.
        {
            //delete the item.
            s_itemsName.DeleteArrayElementAtIndex(item.FindPropertyRelative("itemId").intValue); //because itemID will always be equal to index.
            s_items.DeleteArrayElementAtIndex(item.FindPropertyRelative("itemId").intValue);

            ReCalculateID(); //method to recalculate the index when an item is deleted.
            return; //ignore the rest of the code below to update the item in the next frame.
        }

        GUILayout.EndHorizontal();

        EditorGUILayout.PropertyField(item.FindPropertyRelative("itemDescription")); //GUILayout.Height(35f)); //editable field for item description.

        GUILayout.BeginHorizontal();
        item.FindPropertyRelative("itemSprite").objectReferenceValue = EditorGUILayout.ObjectField("Item Sprite: ",
            item.FindPropertyRelative("itemSprite").objectReferenceValue, typeof(Sprite), false); //to make the image appear in the inspector.
        //only allowed from asset folder.
        
        item.FindPropertyRelative("itemCursor").objectReferenceValue = EditorGUILayout.ObjectField("Item Cursor: ",
            item.FindPropertyRelative("itemCursor").objectReferenceValue, typeof(Texture2D), false); //to make the image appear in the inspector.
        //only allowed from asset folder.

        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
    }

    void ReCalculateID() 
    {
        for (int i = 0; i < s_items.arraySize; i++) //corrects all the item IDs, if you delete one in the middle.
        {
            s_items.GetArrayElementAtIndex(i).FindPropertyRelative("itemId").intValue = i;
            s_itemsName.GetArrayElementAtIndex(i).stringValue = 
                s_items.GetArrayElementAtIndex(i).FindPropertyRelative("itemName").stringValue;
        }
    }
}
