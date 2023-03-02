using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{
    Inventory source;
    SerializedProperty s_inventory, s_itemDatabase;
    int itemId;

    private void OnEnable()
    {
        source = (Inventory)target;

        s_inventory = serializedObject.FindProperty("inventory"); //find inventory list from inventory.cs
        s_itemDatabase = serializedObject.FindProperty("itemDatabase");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(s_itemDatabase);

        if (source.ItemDatabase != null) //only draw the inventory editor if itemdatabase object exists.
        {
            itemId = EditorGUILayout.Popup(itemId, source.ItemDatabase.ItemsNames.ToArray()); //whenever we change this popup it will assign
            //the new itemID to out itemID. Have to convert from list to array.

            if (GUILayout.Button("Add Item"))
            {
                Item newItem = Extensions.CopyItem(source.ItemDatabase.GetItem(itemId)); //here is where one of the public static functions
                //is used. itemID is sourced from the popup.
                source.AddItem(newItem);
            }

            for (int i = 0; i < s_inventory.arraySize; i++)
            {
                //draw every item entry
                DrawItemEntry(s_inventory.GetArrayElementAtIndex(i), i);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    void DrawItemEntry(SerializedProperty item, int id) //copied from itemdatabase editor but modified, int id is to fix an issue where the
        //id is no.
    {
        GUILayout.BeginVertical("box");

        GUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Item Id: " + item.FindPropertyRelative("itemId").intValue, GUILayout.Width(75f));
        EditorGUILayout.LabelField("Item Name: " + item.FindPropertyRelative("itemName").stringValue);

        if (GUILayout.Button("X", GUILayout.Width(20f)))
        {
            //delete the item
            s_inventory.DeleteArrayElementAtIndex(id);

            return;
        }

        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("Item Description: " + item.FindPropertyRelative("itemDescription").stringValue, GUILayout.Height(70f));
        GUILayout.Label(item.FindPropertyRelative("itemDescription").stringValue, EditorStyles.wordWrappedLabel); //so that the text word
        //wraps.

        GUILayout.BeginHorizontal();

       //var spriteViewer = AssetPreview.GetAssetPreview(item.FindPropertyRelative("itemSprite").objectReferenceValue); //texture containing
        //the sprite preview.
       // GUILayout.Label(spriteViewer); //label can render text or a texture. taken out because too big.

        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
    }
}
