using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Loader))]
[RequireComponent(typeof(MinerAnimator))]
public class Router : MonoBehaviour
{
    private Mover _mover;
    private Loader _loader;
    private MinerAnimator _animator;
    private Vector3 _basePosition;
    private Vector3 _waitingPoint;
    private Ore _ore;

    public event Action TransferFinished;
    public event Action ArrivedToBuildPoint;

    public void SetBasePosition(Vector3 position) => _basePosition = position;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _loader = GetComponent<Loader>();
        _animator = GetComponent<MinerAnimator>();
    }
    public void SetWaitingPoint(Vector3 position)
    {
        _waitingPoint = position;

        GoToWaitingPoint();
    }

    public void GoToOre(Ore ore)
    {
        _ore = ore != null ? ore : throw new ArgumentNullException(nameof(ore));

        _mover.ArrivedAtPoint -= FinishTransfer;

        _mover.StartMove(ore.transform.position);
        _animator.Move();

        _mover.ArrivedAtPoint += PickUpOre;
    }

    private void PickUpOre()
    {
        _mover.ArrivedAtPoint -= PickUpOre;
        _loader.ActionCompleted += GoToUploadPoint;
        _loader.PickUp(_ore);
        _animator.PickUpOre();
    }

    public void GoToUploadPoint() // »—œŒÀ‹«”≈“—ﬂ ¿Õ»Ã¿“Œ–ŒÃ!
    {
        float indentForUpload = 1.2f;
        var direction = (_basePosition - transform.position).normalized;
        var point = _basePosition - direction * indentForUpload;

        _loader.ActionCompleted -= GoToUploadPoint;

        _mover.StartMove(point, true);

        _mover.ArrivedAtPoint += StartUnloadOre;
    }

    private void StartUnloadOre()
    {
        _mover.ArrivedAtPoint -= StartUnloadOre;
        _loader.ActionCompleted += FinishUnload;

        _loader.UnloadOre();
        _animator.PutOre();
    }

    private void FinishUnload()
    {
        _loader.ActionCompleted -= FinishUnload;
        GoToWaitingPoint();
    }

    public void GoToWaitingPoint() // »—œŒÀ‹«”≈“—ﬂ ¿Õ»Ã¿“Œ–ŒÃ!
    {
        _mover.StartMove(_waitingPoint);
        _animator.Move();

        TransferFinished?.Invoke();

        _mover.ArrivedAtPoint += FinishTransfer;
    }

    public void GoToPointOfBuild(Vector3 pointOfBuild)
    {
        float indentFromNewBase = 1.2f;
        var direction = (pointOfBuild - transform.position).normalized;
        var point = pointOfBuild - direction * indentFromNewBase;

        _mover.StartMove(point, true);
        _animator.Move();

        _mover.ArrivedAtPoint += BuildBase;
    }

    private void FinishTransfer()
    {
        _mover.ArrivedAtPoint -= FinishTransfer;
        _animator.Wait();
    }

    private void BuildBase()
    {
        _mover.ArrivedAtPoint -= BuildBase;
        ArrivedToBuildPoint?.Invoke();

        _animator.Build();
    }
}