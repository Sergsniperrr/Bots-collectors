using System.Collections.Generic;
using UnityEngine;

public class Pricelist : MonoBehaviour
{
    [SerializeField] private List<Ore> _minerPrice;
    [SerializeField] private List<Ore> _basePrice;

    private Dictionary<string, int> _miner;
    private Dictionary<string, int> _base;

    public Dictionary<string, int> MinerPrice => new(_miner);
    public Dictionary<string, int> BasePrice => new(_base);

    private void Awake()
    {
        _miner = CreatePrice(_minerPrice);
        _base = CreatePrice(_basePrice);
    }

    private Dictionary<string, int> CreatePrice(List<Ore> ores)
    {
        Dictionary<string, int> price = new();
        int minCount = 1;

        foreach (Ore ore in ores)
        {
            if (price.ContainsKey(ore.Name))
                price[ore.Name] += minCount;
            else
                price.Add(ore.Name, minCount);
        }

        return price;
    }
}
