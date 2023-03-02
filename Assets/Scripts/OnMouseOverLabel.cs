
//Attach this script to a GameObject to have it output messages when your mouse hovers over it.
using UnityEngine;

public class OnMouseOverLabel : MonoBehaviour
{

    private GameObject _labelText;
    //private bool _viewAll;
    private new string name;

    void Start()
    {
        //name = GetComponentInChildren<Canvas>().name;
        //_labelText = GameObject.Find(name);

        _labelText = GetComponentInChildren<Canvas>().gameObject;
        
        _labelText.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
            _labelText.SetActive(true);
        else if (Input.GetKeyUp(KeyCode.Tab))
            _labelText.SetActive(false);
        

    }
    
    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");

        _labelText.SetActive(true);
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
        
        _labelText.SetActive(false);
    }
}