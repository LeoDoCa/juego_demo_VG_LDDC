using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    private CharacterController controller;
    public Transform cameraTransform;
    private float gravity;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            gravity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            gravity += Physics.gravity.y * Time.deltaTime;
        }
        
        var gravityVector = Vector3.up * gravity;
        
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var cameraForward = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z);
        var cameraRight = new Vector3(cameraTransform.right.x, 0, cameraTransform.right.z);
        var direction = cameraForward * vertical + cameraRight * horizontal;
        controller.Move((direction.normalized + gravityVector) * Time.deltaTime);
    }
}
