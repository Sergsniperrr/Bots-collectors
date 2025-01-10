using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MinerSearcher))]
public class MinersHandler : MonoBehaviour
{
    [SerializeField] private float _areaRadius = 4f;

    private List<Miner> _miners = new();
    private Miner _minerPrefab;
    private IContainer _mainBase;
    private Miner _freeMiner;

    public void InitializeData(Miner prefab, IContainer mainBase)
    {
        _minerPrefab = prefab != null ? prefab : throw new ArgumentNullException(nameof(prefab));
        _mainBase = mainBase ?? throw new ArgumentNullException(nameof(mainBase));
    }

    public void CreateMiner()
    {
        if (_minerPrefab == null)
            throw new NullReferenceException(nameof(_minerPrefab));

        int newCount = _miners.Count + 1;
        Vector3[] waitingPoints = WaitingPointCreator.Create(transform.position, newCount, _areaRadius);

        _miners.Add(Instantiate(_minerPrefab, waitingPoints[^1], _minerPrefab.transform.rotation));
        _miners[^1].transform.SetParent(transform);
        _miners[^1].SetMainBase(_mainBase);

        UpdateWaitingPoints();
    }

    public void CollectOre(Ore ore)
    {
        _freeMiner = MinerSearcher.FindNearestFreeMiner(_miners.ToArray(), ore);

        if (_freeMiner != null)
            _freeMiner.Collect(ore);
    }

    private void UpdateWaitingPoints()
    {
        if (_miners.Count == 0)
            return;

        Vector3[] waitingPoints = WaitingPointCreator.Create(transform.position, _miners.Count, _areaRadius);

        for (int i = 0; i < _miners.Count; i++)
            _miners[i].SetWaitingPoint(waitingPoints[i]);
    }
}