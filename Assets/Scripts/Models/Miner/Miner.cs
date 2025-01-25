using System;
using UnityEngine;

[RequireComponent(typeof(MinerActioner))]
[RequireComponent(typeof(Router))]
[RequireComponent(typeof(Builder))]
public class Miner : MonoBehaviour
{
    private MinerActioner _loader;
    private Router _router;
    private Builder _builder;

    private IContainer _mainBase;

    public bool IsFree { get; private set; } = true;

    private void Awake()
    {
        _loader = GetComponent<MinerActioner>();
        _router = GetComponent<Router>();
        _builder = GetComponent<Builder>();
    }

    private void Start()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    public void SetWaitingPoint(Vector3 point) => _router.SetWaitingPoint(point);

    public void SetMainBase(IContainer mainBase)
    {
        _mainBase = mainBase ?? throw new ArgumentNullException(nameof(mainBase));
        _router.SetBasePosition(mainBase.Position);
        _loader.SetStore(_mainBase);
    }

    public void Collect(Ore ore)
    {
        SetTarget(ore);
        ore.Disable();

        IsFree = false;

        _router.GoToOre(ore);
        _router.TransferFinished += Release;
    }

    public void GoToNewBase(PreBase preBase, IColonizable newBase)
    {
        _builder.BuildNewBase(preBase, newBase);

        _builder.BuildCompleted += JoinToNewBase;
    }

    private void JoinToNewBase(IColonizable newBase)
    {
        _builder.BuildCompleted -= JoinToNewBase;

        newBase.AddMiner(this);
    }

    private void SetTarget(Ore ore)
    {
        if (IsFree)
        {
            ore.Disable();
            IsFree = false;
        }
    }

    private void Release()
    {
        _router.TransferFinished -= Release;

        IsFree = true;
    }
}