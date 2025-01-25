using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(MinerActioner))]
[RequireComponent(typeof(MinerAnimator))]
public class Router : MonoBehaviour
{
    private Mover _mover;
    private MinerActioner _actioner;
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
        _actioner = GetComponent<MinerActioner>();
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
        _actioner.PickUp(_ore);
        _animator.PickUpOre();
    }

    public void GoToUploadPoint() // »—œŒÀ‹«”≈“—ﬂ ¿Õ»Ã¿“Œ–ŒÃ!
    {
        float indentForUpload = 1.2f;
        var direction = (_basePosition - transform.position).normalized;
        var point = _basePosition - direction * indentForUpload;

        _mover.StartMove(point, true);

        _mover.ArrivedAtPoint += UnloadOre;
    }

    private void UnloadOre()
    {
        _mover.ArrivedAtPoint -= UnloadOre;

        _actioner.UnloadOre();
        _animator.PutOre();
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
        _actioner.Build();
    }
}