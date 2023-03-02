using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actions : MonoBehaviour
{
    public abstract void Act(); //every action derived from the actions class will have the Act() function, which can be different
    //each time.
}
