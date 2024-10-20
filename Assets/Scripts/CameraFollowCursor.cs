using UnityEngine;

public class CameraFollowCursor : MonoBehaviour
{
    public float sensitivity = 5f; 
    public float minX = -30f; 
    public float maxX = 30f; 

    private float rotationX = 0f; 
    private float rotationY = 0f; 

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, minX, maxX);

        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
    }
}
