using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public Transform spaceship;
    public Transform cameraObject;
    public Vector3 offset;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (spaceship.position - cameraObject.position).normalized;
        
        cameraObject.rotation = Quaternion.LookRotation(direction);
        
        transform.Rotate(Vector3.up, Input.mousePositionDelta.x * 0.5f);
        
        transform.position = Vector3.Lerp(transform.position, spaceship.position, Time.deltaTime*5); // De donde salgo, a donde voy, y el incremento
        
        cameraObject.localPosition = offset;
    }
}
