using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance {get; private set;}
    private int coins;
    private int maxCoins = 100;
    public GameObject coinPrefab;

    private void Awake()
    {
        if (Instance) Destroy(Instance.gameObject);
        Instance = this;
    }

    public void AddCoins(int amount)
    {
        coins = Mathf.Min(maxCoins, coins + amount);
        Debug.Log($"Total coins: {coins}");
    }
    
    private void Start()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int z = 0; z < 10; z++)
            {
                var instance = Instantiate(coinPrefab);
                instance.transform.position = new Vector3(x, 0.6f, z);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
