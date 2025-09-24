using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float jumpForce;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(x: 0f, y: jumpForce, z: 0f, ForceMode.VelocityChange);
        }
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var direction = new Vector3(horizontal, 0f, vertical).normalized;
        rb.AddForce(direction * (Time.deltaTime * jumpForce), ForceMode.VelocityChange);
    }
}
