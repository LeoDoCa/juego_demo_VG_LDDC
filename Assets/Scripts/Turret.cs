using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret : MonoBehaviour
{
    
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform bulletPool;
    private List<Bullet>  bullets = new();
    
    IEnumerator Start()
    {
        for (int i = 0; i < 20; i++)
        {
            var instance = Instantiate(bulletPrefab, bulletPool);
            bullets.Add(instance.GetComponent<Bullet>());
            instance.SetActive(false);
        }
        
        while (true)
        {
            var available = bullets.FirstOrDefault(b => !b.gameObject.activeInHierarchy);
            if (available != null)
            {
                available.gameObject.SetActive(true);
                available.transform.position = firePoint.position;
                available.direction = firePoint.up;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
    
    void Update()
    {
        
    }
}
