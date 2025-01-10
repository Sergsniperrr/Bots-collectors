using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OreStacker))]
public class Store : MonoBehaviour, IOreCounter
{
    private const int MinCount = 1;

    private readonly Dictionary<string, int> _oresCounter = new();

    private OreStacker _stacker;

    public event Action<Dictionary<string, int>> OreAdded;

    private void Awake()
    {
        _stacker = GetComponent<OreStacker>();
    }

    public void Add(Ore ore)
    {
        ore.transform.SetParent(transform);

        if (ore.gameObject.TryGetComponent(out Collider collider))
            Destroy(collider);

        _stacker.Put(ore);
        IncrementCounter(ore);

        if (ore.gameObject.TryGetComponent(out Ore oreComponent))
            Destroy(oreComponent);
    }

    private void IncrementCounter(Ore ore)
    {
        if (_oresCounter.ContainsKey(ore.Name))
            _oresCounter[ore.Name] += MinCount;
        else
            _oresCounter.Add(ore.Name, MinCount);

        OreAdded?.Invoke(new Dictionary<string, int>(_oresCounter));
    }
}
