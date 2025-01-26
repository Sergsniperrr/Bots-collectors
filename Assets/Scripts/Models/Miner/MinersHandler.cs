using System;
using System.Collections.Generic;
using UnityEngine;

public class MinersHandler : MonoBehaviour
{
    [SerializeField] private float _areaRadius = 4f;

    private readonly int _minMinersCount = 2;

    private List<Miner> _miners = new();
    private IContainer _mainBase;
    private Miner _freeMiner;
    private MinerSpawner _spawner;

    private void Awake()
    {
        _spawner = transform.parent.GetComponentInChildren<MinerSpawner>();

        if (_spawner == null)
            throw new NullReferenceException(nameof(_spawner));
    }

    private void OnEnable()
    {
        _spawner.MinerBeenCreated += AddMiner;
    }

    private void OnDisable()
    {
        _spawner.MinerBeenCreated -= AddMiner;
    }

    public void InitializeData(Miner prefab, IContainer mainBase)
    {
        _mainBase = mainBase ?? throw new ArgumentNullException(nameof(mainBase));

        _spawner.InitializeData(prefab);
    }

    public void CreateColonist(PreBase preBase, IColonizable newBase)
    {
        if (_miners.Count >= _minMinersCount)
            _spawner.StartCreatingColonist(preBase, newBase);
    }

    public void CreateMiner()
    {
        _spawner.FreeCreateMiner();
    }

    public void AddMiner(Miner miner)
    {
        if (miner == null)
            return;

        miner.transform.SetParent(transform);
        miner.SetMainBase(_mainBase);
        _miners.Add(miner);

        UpdateWaitingPoints(WaitingPointCreator.Create(transform.position, _miners.Count, _areaRadius));
    }

    public void CollectOre(Ore ore)
    {
        _freeMiner = MinerSearcher.FindNearestFreeMiner(_miners.ToArray(), ore);

        if (_freeMiner != null)
            _freeMiner.Collect(ore);
    }

    private void UpdateWaitingPoints(Vector3[] points)
    {
        if (_miners.Count == 0)
            return;

        for (int i = 0; i < _miners.Count; i++)
            _miners[i].SetWaitingPoint(points[i]);
    }
}