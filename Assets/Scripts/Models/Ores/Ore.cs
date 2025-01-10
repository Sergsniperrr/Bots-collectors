using System;
using UnityEngine;

public abstract class Ore : MonoBehaviour, IOreNameable
{
    public event Action<Ore> Died;

    public abstract string Name { get; }
    public bool IsEnable { get; private set; } = true;
    public void Disable() => IsEnable = false;
}