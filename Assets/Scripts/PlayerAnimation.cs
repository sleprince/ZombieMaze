using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation //as it doesn't derive from Monobehaviour we can't use methods such as getcomponent or find
//objects of type, so we do this in PlayerScript instead, with the help of Init method below. This is all to minimise the
//the use of the Monobehaviour class in a GameObject.
{

    private Animator animator;
    private int animSpeed = Animator.StringToHash("Speed"); //create hash id
    
    // use this for initialisation
    public void Init(Animator anim)
    {
        animator = anim;
    }


    public void UpdateAnimation(float speed)
    {
        animator.SetFloat(animSpeed, speed);
    }
}
