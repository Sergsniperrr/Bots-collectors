using System;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public void PickUp(Ore ore)
    {
        Vector3 inventoryPosition = new(0f, 1.25f, 0f);

        ore.transform.SetParent(transform);

        ore.transform.localPosition = inventoryPosition;
    }

    public void Upload(Ore ore, IContainer mainBase)
    {
        if (ore == null)
            throw new ArgumentNullException(nameof(ore));
        
        if (mainBase == null)
            throw new ArgumentNullException(nameof(mainBase));

        mainBase.AddToStore(ore);
    }
}
