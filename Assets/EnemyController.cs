using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class EnemyController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public CinemachineVirtualCamera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public float sensitivity = 5f;
    private float rotY = 0;

    [HideInInspector]
    public bool canMove = true;

    [SerializeField] PlayerAnimations playerAnim;
    //public Animator animator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward) + (right);


            moveDirection.y = movementDirectionY;
        

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

    }
}