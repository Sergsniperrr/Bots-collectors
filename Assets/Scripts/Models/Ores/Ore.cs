using UnityEngine;

public class Ore : MonoBehaviour, IOreNameable
{
    [field: SerializeField] public string Name { get; private set; }
    public bool IsEnable { get; private set; } = true;
    public void Disable() => IsEnable = false;
}