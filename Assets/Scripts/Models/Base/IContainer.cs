using UnityEngine;

public interface IContainer
{
    Vector3 Position { get; }

    void AddToStore(Ore ore);
}
