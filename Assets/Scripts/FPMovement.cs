using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPMovement : MonoBehaviour
{
    public static FPMovement instance;

    public Camera playerCamera;
        
    public float walkSpeed;
    public float runSpeed;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;


    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    public AudioSource footstepsRunning;
    public AudioSource footstepsWalking;


    public bool moving;

    CharacterController characterController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
       
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SetSpeed(float w, float r)
    {
        walkSpeed = w;
        runSpeed = r;
    }


    void Update()
    {

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        characterController.Move(moveDirection * Time.deltaTime);


        if (canMove == true)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        
        if(characterController.velocity.magnitude >= .1)
        {
            if (isRunning)
            {
                footstepsRunning.enabled = true;
                footstepsWalking.enabled = false;
            }
            else
            {
                footstepsRunning.enabled = false;
                footstepsWalking.enabled = true;
            }
            
        }
        else
        {
            footstepsRunning.enabled = false;
            footstepsWalking.enabled = false;
        }

    }

}
