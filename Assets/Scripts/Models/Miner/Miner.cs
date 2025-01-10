using System;
using UnityEngine;

[RequireComponent(typeof(Loader))]
[RequireComponent(typeof(Router))]
public class Miner : MonoBehaviour
{
    private Loader _loader;
    private Router _router;

    private IContainer _mainBase;
    private Ore _target;
    private Ore _oreInInventory;

    public bool IsFree { get; private set; } = true;

    public void SetWaitingPoint(Vector3 point) => _router.SetWaitingPoint(point);

    private void Awake()
    {
        _loader = GetComponent<Loader>();
        _router = GetComponent<Router>();
    }

    public void SetMainBase(IContainer mainBase)
    {
        _mainBase = mainBase ?? throw new ArgumentNullException(nameof(mainBase));
        _router.SetBasePosition(mainBase.Position);
    }

    public void Collect(Ore ore)
    {
        SetTarget(ore);
        ore.Disable();

        IsFree = false;

        _router.GoToOre(ore.transform.position);

        _router.ArrivedToOre += PickUp;
    }

    private void SetTarget(Ore ore)
    {
        if (IsFree)
        {
            _target = ore != null ? ore : throw new ArgumentNullException(nameof(ore));

            ore.Disable();
            IsFree = false;
        }
    }

    private void PickUp()
    {
        _router.ArrivedToOre -= PickUp;

        _loader.PickUp(_target);
        _oreInInventory = _target;

        _router.GoToUploadPoint();

        _router.ArrivedToUploadPoint += UploadOre;
    }

    private void UploadOre()
    {
        _router.ArrivedToUploadPoint -= UploadOre;

        _loader.Upload(_oreInInventory, _mainBase);

        _oreInInventory = null;
        _target = null;
        IsFree = true;

        _router.GoToWaitingPoint();
    }
}