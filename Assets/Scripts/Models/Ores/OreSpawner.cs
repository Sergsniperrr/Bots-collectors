using UnityEngine;

public class OreSpawner : MonoBehaviour
{
    [SerializeField] private Ore[] _prefabs;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _radius = 15f;

    private float _waitingCounter;

    private void Awake()
    {
        if (_prefabs.Length == 0)
            throw new System.ArgumentOutOfRangeException(nameof(_prefabs));
    }

    private void Update()
    {
        _waitingCounter += Time.deltaTime;

        if (_waitingCounter >= _spawnDelay)
        {
            Spawn();
            _waitingCounter = 0f;
        }
    }

    private void Spawn()
    {
        Ore ore = GetRandomPrefab();
        Vector3 spawnPoint = GetRandomPosition();
        int halfDivider = 2;

        spawnPoint.y += ore.transform.localScale.y / halfDivider;

        Instantiate(ore, spawnPoint, ore.transform.rotation, transform);
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = Vector3.zero;

        randomPosition.x = Random.Range(-_radius, _radius);
        randomPosition.z = Random.Range(-_radius, _radius);

        return randomPosition;
    }

    private Ore GetRandomPrefab()
    {
        return _prefabs[Random.Range(0, _prefabs.Length)];
    }
}