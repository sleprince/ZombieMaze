using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Animator))]//need to remember to tag it with this.
public class AnimateAction : Actions
{
    [SerializeField] private List<AnimParameter> anims = new List<AnimParameter>(); // init list of animation parameters.

    [SerializeField] private List<Actions> actions = new List<Actions>(); //initialised list which will be all the animation
    //actions.

    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        
        //init animations.
        for (int i = 0; i < anims.Count; i++)
        {
            anims[i].InitHashID(); //run the init function we just made below, creates the HashIDs.
        }
        
        
    }
    

    public override void Act()
    {
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        int i = 0; // below not using a loop because we want to halt the coroutine whenever the animation is not finished yet.

            while (i <anims.Count)
            {
                yield return new WaitForSeconds(anims[i].InvokeDelay); //after waiting for the animation to finish we want to trigger
                //the next animation.
                
                animator.SetTrigger(anims[i].HashID);

                i++;

                yield return null; //after setting the trigger we want to increase i and halt the coroutine at this point.
            }

        for (int j = 0; j < actions.Count; j++) //outside of the while we want to loop through our animation actions.
        {
            actions[j].Act();
        }
        
    }
    
}//class end.

[System.Serializable]
public class AnimParameter
{
    [SerializeField] private string triggerName;
    [SerializeField] private float invokeDelay; //we only want to set these values in the inspector.

    public float InvokeDelay { get { return invokeDelay; } } //so that we can access this private property in the coroutine above.

    public int HashID { get; private set; } //integer used to access the triggerName. converting it to a HashID using a special function
    //from the animator class. Though public, we've made it only gettable, can't be edited by other scripts.

    public void InitHashID()
    {
        HashID = Animator.StringToHash(triggerName); //we want to pass in the HashIDs to use in a function to transition from
        //one animation state to another.
    }
    
}
