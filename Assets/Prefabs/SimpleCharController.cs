using Cinemachine;
using UnityEngine;


public class SimpleCharController : MonoBehaviour
{
    public static SimpleCharController singleton; //static is a singleton, one instance only

    private float speed = 5f;
    private float jumpSpeed = 10f;
    private float gravity = 20f;

    public float sensitivity = 5f;
    private float rotY = 0;

    private bool isJumping = false;



    private Vector3 moveDirection = Vector3.zero;

    [SerializeField] CharacterController plController;
    [SerializeField] CharacterController vrController;

    public CinemachineVirtualCamera virtualCamera;

    public Animator animator;

    [SerializeField] PlayerAnimations playerAnim;

    //[SerializeField] public bool usingVR; //old way

    [SerializeField] GameManager game;
    [SerializeField] Rigidbody rb;


    void Awake()
    {
        singleton = this;
        //DontDestroyOnLoad(this);

        // Set the initial rotation of the camera
       // transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    public static SimpleCharController GetInstance()
    {
        return singleton;
    }


    void Start()
    {
        //rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Set the initial rotation of the camera
        transform.localEulerAngles = new Vector3(0, 0, 0);

        /*
        if (game.usingVR)
            virtualCamera.enabled = false; //turn off virtual cam if using VR
        else
            virtualCamera.enabled = true;
        */



    }

    void Update()
    {
        {
            if (plController.isGrounded)
            {
                // grounded movement code
                moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
                moveDirection = moveDirection.normalized * speed * 2f;

                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                    //rb.AddForce(moveDirection); doesn't work
                }
            }
            else
            {
                // mid-air movement code
                moveDirection += (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
                moveDirection = Vector3.ClampMagnitude(moveDirection, speed * 2f);
            }

            moveDirection.y -= gravity * Time.deltaTime;
            plController.Move(moveDirection * Time.deltaTime);

            float rotX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
            rotY += Input.GetAxis("Mouse Y") * sensitivity;
            rotY = Mathf.Clamp(rotY, -90f, 90f);

            transform.localEulerAngles = new Vector3(0, rotX, 0);
            virtualCamera.transform.localEulerAngles = new Vector3(-rotY, rotX, 0);

            playerAnim.UpdateAnimation(plController.velocity.sqrMagnitude * 5);
        }

    }

}
    
