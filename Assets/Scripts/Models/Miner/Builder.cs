using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Router))]
[RequireComponent(typeof(ToolsView))]
public class Builder : MonoBehaviour
{
    [SerializeField] private float _buildDuration;

    private readonly float _zeroPositionY = 0.13f;

    private Router _router;
    private ToolsView _toolsView;
    private PreBase _preBase;
    private IColonizable _newBase;

    public event Action<IColonizable> BuildCompleted;

    private void Awake()
    {
        _router = GetComponent<Router>();
        _toolsView = GetComponent<ToolsView>();
    }

    public void BuildNewBase(PreBase preBase, IColonizable newBase)
    {
        _preBase = preBase != null ? preBase : throw new ArgumentNullException(nameof(preBase));
        _newBase = newBase ?? throw new ArgumentNullException(nameof(newBase));

        _router.GoToPointOfBuild(preBase.transform.position);

        _router.ArrivedToBuildPoint += StartBuild;
    }

    private void StartBuild()
    {
        _router.ArrivedToBuildPoint -= StartBuild;

        _toolsView.ShowInstruments();

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
            positionY = Mathf.MoveTowards(_newBase.Position.y, _zeroPositionY, Time.deltaTime * speed);
            _newBase.SetPositionY(positionY);

            yield return null;
        }

        Destroy(_preBase.gameObject);
        _toolsView.HideInstruments();
        _router.GoToWaitingPoint();

        BuildCompleted?.Invoke(_newBase);
    }
}
