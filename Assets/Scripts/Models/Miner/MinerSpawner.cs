using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Buyer))]
public class MinerSpawner : MonoBehaviour
{
    private Miner _minerPrefab;
    private PreBase _preBase;
    private Buyer _buyer;
    private Store _store;
    private IColonizable _newBase;
    private Vector3 _spawnPosition = new(0f, 0f, 0f);

    public event Action<Miner> MinerBeenCreated;

    public bool IsColonistCreation { get; private set; } = false;

    private void Awake()
    {
        InitializeData();
    }

    private void OnEnable()
    {
        _store.OreCountChanged += Create;
    }

    private void OnDisable()
    {
        _store.OreCountChanged -= Create;
    }

    public void InitializeData(Miner prefab)
    {
        _minerPrefab = prefab != null ? prefab : throw new ArgumentNullException(nameof(prefab));
    }

    public void FreeCreateMiner()
    {
        Miner miner = Instantiate(_minerPrefab, _spawnPosition, transform.rotation);

        MinerBeenCreated?.Invoke(miner);
    }

    public void CreateMiner()
    {
        if (_buyer.BuyMiner(_store) == false)
            return;

        FreeCreateMiner();
    }

    public void StartCreatingColonist(PreBase preBase, IColonizable newBase)
    {
        _preBase = preBase != null ? preBase : throw new ArgumentNullException(nameof(preBase));
        _newBase = newBase ?? throw new ArgumentNullException(nameof(newBase));

        IsColonistCreation = true;

        CreateColonist(preBase, newBase);
    }

    public void CreateColonist(PreBase preBase, IColonizable newBase)
    {
        IsColonistCreation = true;

        if (_buyer.BuyColonist(_store) == false)
            return;

        Miner colonist = Instantiate(_minerPrefab, _spawnPosition, transform.rotation);

        IsColonistCreation = false;

        colonist.GoToNewBase(preBase, newBase);
    }

    public void Create(Dictionary<string, int> _)
    {
        if (IsColonistCreation)
            CreateColonist(_preBase, _newBase);
        else
            CreateMiner();
    }

    private void InitializeData()
    {
        _buyer = GetComponent<Buyer>();
        _store = transform.parent.GetComponentInChildren<Store>();

        if (_store == null)
            throw new NullReferenceException(nameof(_store));
    }
}
