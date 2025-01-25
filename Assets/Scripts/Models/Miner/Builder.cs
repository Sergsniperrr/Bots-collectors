using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Router))]
public class Builder : MonoBehaviour
{
    [SerializeField] private float _buildDuration;

    private readonly float _zeroPositionY = 0.13f;

    private Router _router;
    private PreBase _preBase;
    private IColonizable _newBase;

    public event Action<IColonizable> BuildCompleted;

    private void Awake()
    {
        _router = GetComponent<Router>();
    }

    public void BuildNewBase(PreBase preBase, IColonizable newBase)
    {
        _preBase = preBase != null ? preBase : throw new ArgumentNullException(nameof(preBase));
        _newBase = newBase != null ? newBase : throw new ArgumentNullException(nameof(newBase));

        _router.GoToPointOfBuild(preBase.transform.position);

        _router.ArrivedToBuildPoint += StartBuild;
    }

    private void StartBuild()
    {
        _router.ArrivedToBuildPoint -= StartBuild;

        _preBase.EnableDust();
        StartCoroutine(BuildingBase());
    }

    private IEnumerator BuildingBase()
    {
        float positionY = 0f;

        float currentPositionY = _newBase.Position.y;
        float distanse = _zeroPositionY - currentPositionY;
        float speed = distanse / _buildDuration;

        while (positionY != _preBase.transform.position.y)
        {
            Mathf.MoveTowards(currentPositionY, _zeroPositionY, Time.deltaTime * speed);

            yield return null;
        }

        Destroy(_preBase.gameObject);

        BuildCompleted?.Invoke(_newBase);
    }
}
