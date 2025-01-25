using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MinerAnimator))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private readonly float _slownessFactor = 0.7f;
    private readonly float _fullSpeedFactor = 1f;
    private readonly float _verticalShift = -0.13f;

    private Vector3 _target;
    private Vector3 _direction;
    private Vector3 _newPosition;
    private Coroutine _coroutine;
    private float _distance;
    private float _moveMultiplier = 1f;
    private bool _canMove = false;

    public event Action ArrivedAtPoint;

    private void Update()
    {
        if (_canMove)
            Move();
    }

    public void StartMove(Vector3 target, bool isWithOre = false)
    {
        _target = target;
        _target.y = _verticalShift;

        StartCoroutine(RotateAfterDelay());

        if (isWithOre)
            _moveMultiplier = _slownessFactor;
        else
            _moveMultiplier = _fullSpeedFactor;
    }

    private IEnumerator RotateAfterDelay()
    {
        yield return null;

        Rotate();
    }

    private void Rotate()
    {
        _target.y = _verticalShift;

        transform.LookAt(_target);

        _canMove = true;
    }

    private void Move()
    {
        _direction = _target - transform.position;
        _direction.y = 0f;
        _distance = _direction.magnitude;
        _direction.Normalize();

        if (_distance < 0.5f)
        {
            _direction = Vector3.zero;

            _canMove = false;

            ArrivedAtPoint?.Invoke();
        }

        _newPosition = transform.position + _speed * _moveMultiplier * Time.deltaTime * _direction;
        transform.position = _newPosition;
    }
}
