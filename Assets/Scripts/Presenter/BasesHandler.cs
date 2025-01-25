using System;
using System.Collections.Generic;
using UnityEngine;

public class BasesHandler : MonoBehaviour
{
    [SerializeField] private BaseSpawner _spawner;
    [SerializeField] private BasesSelecter _selecter;
    [SerializeField] private MouseInputHandler _input;
    [SerializeField] private BaseDemo _baseDemoPrefab;

    private Base _activeBase;
    private BaseDemo _baseDemo;
    private Vector3 _newBaseCoordinate;

    private void OnEnable()
    {
        _selecter.ActiveBaseChanged += GetActiveBase;
    }

    private void OnDisable()
    {
        _selecter.ActiveBaseChanged -= GetActiveBase;
    }

    private void Update()
    {
        if (_activeBase == null)
            return;

        if (_baseDemo == null)
            return;

        _baseDemo.transform.position = _input.OnMouseMove();

        if (_input.OnRightClick())
            BuildBase(_baseDemo.transform.position);

    }

    public void BuildBase(Vector3 position) => _spawner.CreateBase(position);

    private void GetActiveBase(Base activeBase)
    {
        if (activeBase == null)
            if (_baseDemo != null)
                Destroy(_baseDemo.gameObject);
        else
            if (_baseDemo == null)
                _baseDemo = Instantiate(_baseDemoPrefab, _input.OnMouseMove(), Quaternion.identity);

        _activeBase = activeBase;
    }
}
