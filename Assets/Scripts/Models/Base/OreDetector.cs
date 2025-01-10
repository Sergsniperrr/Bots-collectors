using System;
using UnityEngine;

public class OreDetector : MonoBehaviour
{
    [SerializeField] private float _radius = 15f;

    private float _searchDelay = 0.5f;
    private float _waitSearchCounter;

    public event Action<Ore> OreDetected;

    private void Update()
    {
        _waitSearchCounter += Time.deltaTime;

        if (_waitSearchCounter >= _searchDelay)
        {
            Scan();
            _waitSearchCounter = 0f;
        }
    }

    private void Scan()
    {
        Vector3 scanSize = new(_radius, 0.01f, _radius);
        Vector3 shift = new(0f, 0.2f, 0f);

        Collider[] hitColliders = Physics.OverlapBox(transform.position + shift, scanSize, Quaternion.identity);

        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject.TryGetComponent(out Ore ore))
            {
                if (ore.IsEnable)
                {
                    OreDetected?.Invoke(ore);
                    return;
                }
            }
        }
    }
}
