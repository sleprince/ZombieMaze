using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI; //needed to use Unity's AI functions.

public class PlayerScript : MonoBehaviour
{

    private NavMeshAgent agent;
    private Camera mainCamera;

    private bool turning; //are they turning.
    private Quaternion targetRot; //rotation value of the target.

    private LineRenderer line; //to use a pseudo visual of what the raycast is doing.

    private PlayerAnimation playerAnim = new PlayerAnimation(); //with this way of doing it we don't need to attach the
    //animator in the editor.

    [SerializeField] private Animator pAnim;

    [SerializeField] private ParticleSystem effect; //an effect that will occur when the player clicks somewhere.

    [SerializeField] private GameObject InventoryUI;

    public NavMeshAgent Agent { get { return agent; } } //public getter.
    
    public Texture2D cursorTexture; //these 3 parameters are for custom mouse cursor for look, pick up etc.
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    private int i = -1;

    public List<MouseOptions> mouseOptions = new List<MouseOptions>();

    public int I { get { return i; } set { i = value; } } //editable value, public getter.


    public static PlayerScript instance;
    public static PlayerScript GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        //getting the reference to the NavMeshAgent in Unity Editor that's attached
        //to the player.
        agent = GetComponent<NavMeshAgent>();

        //getting the reference to the MainCamera in the scene.
        //need to make sure the camera is tagged as MainCamera in editor.
        mainCamera = Camera.main;

        line = GetComponent<LineRenderer>();
        
        playerAnim.Init(GetComponentInChildren<Animator>()); //will search for components of type animator, since it's a
        //method we need to add parenthesis.
        
        //playerAnim.Init(pAnim);
        
        Cursor.SetCursor(mouseOptions[3].cursor, hotSpot, cursorMode); //set cursor to walk to start with.

    }

    // Update is called once per frame
    void Update()
    {
        agent.speed = 7f;


        if (Input.GetMouseButtonDown(1)) //right mouse button to cycle between different cursors for different action types.
        {
            
            i++;
            if (i == 4)
                i = 0;
            Cursor.SetCursor(mouseOptions[i].cursor, hotSpot, cursorMode);
            

        }

        if (Input.GetKeyDown(KeyCode.I))
            if(!InventoryUI.activeSelf)
                InventoryUI.SetActive(true);
            else
            {
                InventoryUI.transform.Find("DescriptionPanel").gameObject.SetActive(false);
                InventoryUI.SetActive(false);
            }
        
        
        if (Input.GetMouseButtonDown(0) && !DialogueSystem.Instance.conversing && !InventoryUI.activeSelf) //if left mouse button clicked.
            //0 is left, 1 is right, 2 is middle.
        {
            OnClick();
        }
        
        if (turning && DialogueSystem.Instance.conversing)
        {
            StartCoroutine(StopTurning());
        }

        if (turning && transform.rotation != targetRot) //if turning and not rotated towards the target.
        {
            //transform towards the target.
            //15f x time is so that the rotation is smooth all different spec PCs with different framerates.
            //targetRot.x -= 10f;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 15f * Time.deltaTime);
            //agent.speed = 0;
            
           // if (DialogueSystem.Instance.panel.activeSelf)
           // {
            //    transform.rotation = transform.rotation;

           // }
            
        }
        
        if (!agent.isStopped && DialogueSystem.Instance.conversing)
            agent.isStopped = true;
        
        //playerAnim
        playerAnim.UpdateAnimation(agent.velocity.sqrMagnitude); //to get float of movement speed from agent, don't need
        //magnitude normal version as sqr is near enough.
        
    }

    void OnClick()
    {
        //Debug.Log("Left mouse button clicked!");

        //variable of type RaycastHit called hit that contains data about where the
        //raycast hit.
        RaycastHit hit; 

        //camToScreen is a variable of type Ray which is the Ray being cast.
        //ScreenPointToRay, this is from our screen position (mouse clicked position), it shoots a ray onto our
        //scene, that ray is called CamToScreen. Ray starts at camera and goes to mouse position.
        Ray camToScreen = mainCamera.ScreenPointToRay(Input.mousePosition);




        //Physics.Raycast, this is a boolean so if the ray hits something all of this
        //code will execute.
        //we pass in our ray, we use Infinity to cover the maximum distance and
        //out passes all the information into hit that is used below such as hit.point
        if (Physics.Raycast(camToScreen, out hit, Mathf.Infinity))
        {

            effect.transform.position = hit.point; //move particle effect to ray hit point and play.
            
            if(hit.collider.gameObject.layer != 2) //if the layer is not ignore raycast layer
                


            //if raycast hits something move player.
            if (hit.collider != null && (hit.collider.gameObject.layer != 2 && hit.collider.gameObject.layer != 5 ) && !InventoryUI.activeSelf) //if what it hit is not null, and not on the ignore raycast layer
            {

                effect.Play();

                //temporary variable interactive is the Interactable class script that is attached to the the 
                //object the raycast is hitting.
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                    //if the player has clicked on an interactable beacuse it's not null.
                    if (interactable != null)
                    {
                        //move player to the interactable *first.
                        MovePlayer(interactable.InteractPosition());
                        interactable.Interact(this); //can use "this" because we are sending this playerscript over.

                        
                        
                    }
                    else
                    {   //the hit point is sent over as targetPosition.
                        MovePlayer(hit.point); //hit.point will be where the player moves to.


                    }

                //line.enabled = true; was for debugging purposes
                line.SetPosition(0, transform.position);
                line.SetPosition(1, hit.point);
                StartCoroutine(DeleteLine());


            }

            else
            {
                effect.Stop();
                line.enabled = false;
            }

        }

    }

    public bool CheckIfArrived()
    {
        //true or false that there is no path pending and the agent has arrived.
        return (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance);
    }

    void MovePlayer(Vector3 targetPosition)
    {
        turning = false;
        
        agent.isStopped = false;

        agent.SetDestination(targetPosition);

        DialogueSystem.Instance.HideDialog();

    }

    public void SetDirection(Vector3 targetDirection)
    {
        turning = true;
        Vector3 vectorDirection = targetDirection - transform.position;
        vectorDirection.y = 0f;
        targetRot = Quaternion.LookRotation(vectorDirection);
    }

    IEnumerator StopTurning()
    {
        yield return new WaitForSeconds(1f);
        turning = false;
    }
    
    
    IEnumerator DeleteLine()
    {
        yield return new WaitForSeconds(0.5f);
        
        line.enabled = false;
        effect.Stop();
        
        while(!CheckIfArrived())
        {
            yield return null; //delays the coroutine while player hasn't arrived.
        }
        

        
        
        Cursor.SetCursor(mouseOptions[3].cursor, hotSpot, cursorMode); //set cursor back to walk after interacting.
        i = 3; //after interacting revert to normal mouse pointer integer.
    }

    [System.Serializable]
    public class MouseOptions

    {
        public  Texture2D cursor;
        public  string cursorType;




    } 
} //class end

