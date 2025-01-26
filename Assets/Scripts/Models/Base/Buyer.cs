using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buyer : MonoBehaviour
{
    [SerializeField] private string[] _baseInitialPrice;
    [SerializeField] private string[] _minerInitialPrice;

    private Dictionary<string, int> _minerPrice;
    private Dictionary<string, int> _basePrice;

    private void Awake()
    {
        _minerPrice = CreatePrice(_minerInitialPrice);
        _basePrice = CreatePrice(_baseInitialPrice);
    }

    public bool BuyMiner(Store store) => Buy(store, _minerPrice);
    public bool BuyColonist(Store store) => Buy(store, _basePrice);

    public bool Buy(Store store, Dictionary<string, int> price)
    {
        bool result = store.CheckOresForEnough(price);

        if (result)
            store.RemoveOres(price);

        return result;
    }
    private Dictionary<string, int> CreatePrice(string[] ores)
    {
        Dictionary<string, int> price = new();
        int minCount = 1;

        foreach (string ore in ores)
        {
            if (price.ContainsKey(ore))
                price[ore] += minCount;
            else
                price.Add(ore, minCount);
        }

        return price;
    }
}
