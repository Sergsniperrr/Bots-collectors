using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Router : MonoBehaviour
{
    private Mover _mover;
    private Vector3 _basePosition;
    private Vector3 _waitingPoint;

    public event Action ArrivedToOre;
    public event Action ArrivedToUploadPoint;

    public void SetBasePosition(Vector3 position) => _basePosition = position;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }
    public void SetWaitingPoint(Vector3 position)
    {
        _waitingPoint = position;

        _mover.StartMoveTo(_waitingPoint);
    }

    public void GoToOre(Vector3 position)
    {
        _mover.StartMoveTo(position);

        _mover.ArrivedAtPoint += FinishMoveToOre;
    }

    public void GoToUploadPoint()
    {
        float indentForUpload = 1.8f;
        var direction = (_basePosition - transform.position).normalized;
        var point = _basePosition - direction*indentForUpload;

        _mover.ArrivedAtPoint -= FinishMoveToOre;

        _mover.StartMoveTo(point);

        _mover.ArrivedAtPoint += FinishMoveToUploadPoint;
    }

    public void GoToWaitingPoint()
    {
        _mover.ArrivedAtPoint -= FinishMoveToUploadPoint;

        _mover.StartMoveTo(_waitingPoint);
    }

    private void FinishMoveToOre()
    {
        ArrivedToOre?.Invoke();
    }

    private void FinishMoveToUploadPoint()
    {
        ArrivedToUploadPoint?.Invoke();
    }
}