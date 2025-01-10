using System;
using UnityEngine;

public interface IObservable
{
    event Action<Transform> CameraInitialized;
}
