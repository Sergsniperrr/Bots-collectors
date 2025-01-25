using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OreStacker))]
public class Store : MonoBehaviour, IOreCounter
{
    private const int MinCount = 1;

    private readonly Dictionary<string, int> _oresCounter = new();

    private OreStacker _stacker;

    public event Action<Dictionary<string, int>> OreCountChanged;

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
    }

    public bool CheckOresForEnough(Dictionary<string, int> requireOres)
    {
        if (_oresCounter.Count == 0)
            return false;

        foreach (string key in requireOres.Keys)
        {
            if (_oresCounter.Keys.Contains(key) == false)
                return false;

            if (_oresCounter[key] < requireOres[key])
                return false;
        }

        return true;
    }

    public void RemoveOres(Dictionary<string, int> requireOres)
    {
        if (CheckOresForEnough(requireOres) == false)
            throw new ArgumentOutOfRangeException(nameof(requireOres));

        foreach (KeyValuePair<string, int> ore in requireOres)
        {
            DecrementCounter(ore);
            _stacker.Remove(ore);
        }

        _stacker.UpdateOresPositions();
    }

    private void IncrementCounter(Ore ore)
    {
        if (_oresCounter.ContainsKey(ore.Name))
            _oresCounter[ore.Name] += MinCount;
        else
            _oresCounter.Add(ore.Name, MinCount);

        OreCountChanged?.Invoke(new Dictionary<string, int>(_oresCounter));
    }

    private void DecrementCounter(KeyValuePair<string, int> ore)
    {
        _oresCounter[ore.Key] -= ore.Value;

        if (_oresCounter[ore.Key] == 0)
            _oresCounter.Remove(ore.Key);

        OreCountChanged?.Invoke(new Dictionary<string, int>(_oresCounter));
    }
}
