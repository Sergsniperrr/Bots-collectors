using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buyer : MonoBehaviour
{
    [SerializeField] private Pricelist _pricelist;

    public bool BuyMiner(Store store) => Buy(store, _pricelist.MinerPrice);
    public bool BuyColonist(Store store) => Buy(store, _pricelist.BasePrice);

    public bool Buy(Store store, Dictionary<string, int> price)
    {
        bool result = store.CheckOresForEnough(price);

        if (result)
            store.RemoveOres(price);

        return result;
    }
}
