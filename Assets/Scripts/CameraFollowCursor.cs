using UnityEngine;

public class CameraFollowCursor : MonoBehaviour
{
    public bool view = false;
    public float sensitivity = 5f; 
    public float minX = -30f; 
    public float maxX = 30f;

    public float minY = -30f;
    public float maxY = 30f;

    private float rotationX = 0f; 
    private float rotationY = 0f; 

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (view == true)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            rotationY += mouseX;
            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, minX, maxX);
            rotationY = Mathf.Clamp(rotationY, minY, maxY);

            transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
        }
        
    }
}
