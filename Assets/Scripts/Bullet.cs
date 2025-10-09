using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 20f;
    private float timer;

    void Update()
    {
        transform.Translate(direction * (speed*Time.deltaTime));
        timer += Time.deltaTime;
        if (timer >= 5f)
        {
            gameObject.SetActive(false);
            timer = 0f;
        }
    }
}
