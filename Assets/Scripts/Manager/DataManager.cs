using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }//singleton, can be used by any script, all code in awake is needed to create a
    //singleton.
    public Inventory Inventory { get { return inventory; } } //inventory public getter, can get but not edit.

    [SerializeField] Inventory inventory; //drag and drop inventory scriptable object here. Now we have access to the inventory from any
    //other script.

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //so that it is not destroyed on loading a new scene.
        }
        else
        {
            Destroy(gameObject); //so that if a gameobject using Instance keyword already exists, the new one gets destroyed.
        }
    }
}