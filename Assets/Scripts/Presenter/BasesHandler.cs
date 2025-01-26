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

    private void OnEnable()
    {
        _selecter.ActiveBaseChanged += GetActiveBase;
        _selecter.Unselect += Unselect;
    }

    private void OnDisable()
    {
        _selecter.ActiveBaseChanged -= GetActiveBase;
        _selecter.Unselect -= Unselect;
    }

    private void Update()
    {
        if (_activeBase == null)
            return;

        if (_baseDemo == null)
            return;

        _baseDemo.transform.position = _input.OnMouseMove();

        if (_input.OnRightClick())
        {
            _spawner.BuildBase(_activeBase, _baseDemo.transform.position);
            Destroy(_baseDemo.gameObject);
        }
    }

    public void BuildBase(Vector3 position) => _spawner.CreateBase(position);

    private void GetActiveBase(Base activeBase)
    {
        if (_baseDemo == null && activeBase != null)
            _baseDemo = Instantiate(_baseDemoPrefab, _input.OnMouseMove(), Quaternion.identity);

        _activeBase = activeBase;
    }

    private void Unselect()
    {
        _activeBase = null;

        if (_baseDemo != null)
            Destroy(_baseDemo.gameObject);
    }
}
