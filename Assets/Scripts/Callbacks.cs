using UnityEngine;

public class Callbacks : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){}

    // Update is called once per frame
    void Update()
    {
        float hotizontal = Input.GetAxis("Horizontal");     // -1 0 1
        float vertical = Input.GetAxis("Vertical");
        
        // transform.position += new Vector3(hotizontal, 0, vertical) * (30*Time.deltaTime);
        
        Vector3 direction = new Vector3(hotizontal, 0, vertical) * (Time.deltaTime * 20);
        
        transform.Translate(direction);
        
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Rotate(0,200*Time.deltaTime,0);
        }
            
    }
}
