using System;
using UnityEngine;

public class BasesSelecter : MonoBehaviour
{
    [SerializeField] private MouseInputHandler _input;
    [SerializeField] private Camera _camera;

    private Collider _collider;
    private Base _baseBuffer;

    public event Action<Base> ActiveBaseChanged;

    private void Update()
    {
        HandleClickedObjects();
    }

    private void HandleClickedObjects()
    {
        _collider = _input.OnLeftClick();

        if (_collider == null)
            return;

        if (_collider.gameObject.TryGetComponent(out Base selectedBase))
            SelectBase(selectedBase);
        else
            UnselectBase();

        ActiveBaseChanged?.Invoke(_baseBuffer);
    }

    private void SelectBase(Base selectedBase)
    {
        if (selectedBase != null)
            selectedBase.HideArea();

        selectedBase.ShowArea();
        _baseBuffer = selectedBase;
    }

    private void UnselectBase()
    {
        if (_baseBuffer == null)
            return;

        _baseBuffer.HideArea();
        _baseBuffer = null;
    }
}
