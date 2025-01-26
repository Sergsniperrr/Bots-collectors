using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

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

    public void Add(Ore ore)
    {
        ore.transform.SetParent(transform);

        _stacker.Put(ore);
        IncrementCounter(ore);

        OreCountChanged?.Invoke(_oresCounter);
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

        OreCountChanged?.Invoke(_oresCounter);
    }

    private void IncrementCounter(Ore ore)
    {
        if (_oresCounter.ContainsKey(ore.Name))
            _oresCounter[ore.Name] += MinCount;
        else
            _oresCounter.Add(ore.Name, MinCount);
    }

    private void DecrementCounter(KeyValuePair<string, int> ore)
    {
        _oresCounter[ore.Key] -= ore.Value;

        if (_oresCounter[ore.Key] == 0)
            _oresCounter.Remove(ore.Key);
    }
}
