using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputHandler : MonoBehaviour
{
    [SerializeField] private Collider _platform;
    [SerializeField] private Camera _mainCamera;

    private const int LeftMouseButton = 0;
    private const int RightMouseButton = 1;

    private readonly float _positionOnY = 0.13f;


    private Ray _ray;
    private RaycastHit _hitInfo;
    private RaycastHit _hit;
    private Vector3 _collisionPoint;

    public Collider OnLeftClick() => OnMouseClick(LeftMouseButton);
    public bool OnRightClick() => OnMouseClick(RightMouseButton) == _platform;

    public Vector3 OnMouseMove()
    {
        _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit, Mathf.Infinity))
        {
            if (_hit.collider == _platform)
            {
                _collisionPoint = _hit.point;
                _collisionPoint.y = _positionOnY;
            }
        }

        return _collisionPoint;
    }

    private Collider OnMouseClick(int mouseButton)
    {
        if (Input.GetMouseButtonDown(mouseButton))
        {
            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hitInfo))
                return _hitInfo.collider;
        }

        return null;
    }
}
