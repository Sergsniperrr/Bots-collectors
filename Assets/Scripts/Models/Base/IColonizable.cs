using UnityEngine;

public interface IColonizable
{
    Vector3 Position { get; }
    void AddMiner(Miner miner);
    void SetPositionY(float value);
    void Enable();
}
