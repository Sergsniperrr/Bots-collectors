using System;
using UnityEngine;

public class TextRotator : MonoBehaviour
{
    private Transform _camera;
    private IObservable _base;
    private Vector3 _rotation;

    private void Awake()
    {
        _base = GetComponentInParent<IObservable>();

        if (_base == null)
            throw new NullReferenceException(nameof(_base));

        _base.CameraInitialized += InitializeCamera;
    }

    private void Update()
    {
        transform.LookAt(_camera);

        _rotation = transform.rotation.eulerAngles;
        _rotation.x = 0f;
        transform.rotation = Quaternion.Euler(_rotation);
    }

    private void InitializeCamera(Transform camera)
    {
        _camera = camera != null ? camera : throw new ArgumentNullException(nameof(camera));

        _base.CameraInitialized -= InitializeCamera;
    }
}
