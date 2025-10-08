using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    /*-----Variables de movimiento-----*/
    private CharacterController controller;
    public Transform cameraTransform;
    private float gravity;
    public float speed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 5f;
    
    /*-----Variables de animación-----*/
    public Animator animator;
    private readonly int moveSpeedHash = Animator.StringToHash("moveSpeed");
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (controller.isGrounded)
        {
            gravity = Physics.gravity.y;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gravity = jumpForce;
            }
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
        controller.Move((direction.normalized * speed + gravityVector) * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            var targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        
        /*-----Código de animación del personaje-----*/
        // direction = x, y, z
        // direction.normalized.magnitude
        var moveSpeedValue = direction.normalized.magnitude;
        animator.SetFloat(moveSpeedHash, moveSpeedValue);
    }
    
    
}
