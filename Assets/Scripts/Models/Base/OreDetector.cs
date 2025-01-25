using System;
using System.Collections;
using UnityEngine;

public class OreDetector : MonoBehaviour
{
    [SerializeField] private float _radius = 10f;

    private Coroutine _coroutine;
    private float _searchDelay = 0.5f;

    public event Action<Ore> OreDetected;

    public void StartScan()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ScanWithDelay(_searchDelay));
    }

    private IEnumerator ScanWithDelay(float delay)
    {
        var wait = new WaitForSeconds(delay);

        while (enabled)
        {
            Scan();

            yield return wait;
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
