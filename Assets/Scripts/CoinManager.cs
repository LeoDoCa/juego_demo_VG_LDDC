using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance {get; private set;}
    private int coins;
    private int maxCoins = 100;

    private void Awake()
    {
        if (Instance) Destroy(Instance.gameObject);
        Instance = this;
    }

    public void AddCoins(int amount)
    {
        coins = Mathf.Min(maxCoins, coins + amount);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
