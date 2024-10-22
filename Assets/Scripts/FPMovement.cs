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

    public CameraFollowCursor cameraFollowCursor;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    public AudioSource footstepsRunning;
    public AudioSource footstepsWalking;

    public bool moving;

    // Añadir variable de gravedad
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;  // Para manejar saltos, si lo necesitas

    // Variable para verificar si está en el suelo
    private bool isGrounded;

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
        // Verificar si el personaje está en el suelo
        isGrounded = characterController.isGrounded;

        // Si está en el suelo y la velocidad en Y es negativa, la reseteamos para que no siga cayendo indefinidamente
        if (isGrounded && moveDirection.y < 0)
        {
            moveDirection.y = -2f;
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Cálculo de velocidad dependiendo si corre o camina
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // Manejo de rotación y movimiento de cámara
        if (cameraFollowCursor.view == false)
        {
            if (canMove == true)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }

            // Manejar el sonido de los pasos
            if (characterController.velocity.magnitude >= 0.1f)
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

        // Aplicar salto si está en el suelo
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //moveDirection.y = Mathf.Sqrt(jumpHeight * -2f * gravity);  // Calculamos la velocidad del salto
        }

        // Aplicar la gravedad siempre
        moveDirection.y += gravity * Time.deltaTime;

        // Mover el Character Controller
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
