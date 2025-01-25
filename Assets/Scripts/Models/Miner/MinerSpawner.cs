using System;
using System.Collections.Generic;
using UnityEngine;

public class MinerSpawner : MonoBehaviour
{
    private Miner _minerPrefab;
    private PreBase _preBase;
    private Buyer _buyer;
    private Store _store;
    private IColonizable _newBase;

    public event Action<Miner> MinerBeenCreated;

    public bool IsColonistCreation { get; private set; } = false;

    private void Awake()
    {
        _store = transform.parent.GetComponentInChildren<Store>();

        if (_store == null)
            throw new NullReferenceException(nameof(_store));
    }

    private void OnEnable()
    {
        _store.OreCountChanged += Create;
    }

    private void OnDisable()
    {
        _store.OreCountChanged -= Create;
    }

    public void InitializeData(Buyer buyer, Miner prefab)
    {
        _buyer = buyer != null ? buyer : throw new ArgumentNullException(nameof(buyer));
        _minerPrefab = prefab != null ? prefab : throw new ArgumentNullException(nameof(prefab));
    }

    public void FreeCreateMiner()
    {
        Miner miner = Instantiate(_minerPrefab, Vector3.zero, transform.rotation);

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

        Miner colonist = Instantiate(_minerPrefab, Vector3.zero, transform.rotation);

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
}
