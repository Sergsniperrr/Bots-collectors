using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField] private Base _basePrefab;
    [SerializeField] private Miner _minerPrefab;
    [SerializeField] private Transform _camera;

    private void Start()
    {
        CreateBase();
    }

    private void CreateBase()
    {
        float offsetY = 0.15f;
        Vector3 position = new(0f, offsetY, 0f);

        Base baseClone = Instantiate(_basePrefab, position, transform.rotation);

        baseClone.transform.SetParent(transform);
        baseClone.InitializeData(_minerPrefab, _camera);
    }
}
