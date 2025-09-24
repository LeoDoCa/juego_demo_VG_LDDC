using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform cameraObject;
    public float speed = 10;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");     // -1 0 1
        float vertical = Input.GetAxisRaw("Vertical");
        
        // transform.position += new Vector3(horizontal, 0, vertical) * (30*Time.deltaTime);
        
        Vector3 direction = cameraObject.forward * vertical + cameraObject.right * horizontal;
        direction = new Vector3(direction.x, 0, direction.z).normalized * (Time.deltaTime*speed);
        // Vector3 direction = new Vector3(horizontal, 0, vertical) * (Time.deltaTime * 20);
        //transform.Translate(direction); //Utiliza ejes locales (toma en cuenta la rotación de la nave) (rotan tambien los ejes)
        transform.position += direction;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = 
                Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);
        }

        // Vector3 directionA = new Vector3(0,1,0) * (Time.deltaTime*1);
        // if (Input.GetKey(KeyCode.Space))
        // {
        //     transform.position += directionA;;
        // }
    }
}
