using System;
using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField] private Base _basePrefab;
    [SerializeField] private Miner _minerPrefab;
    [SerializeField] private Transform _camera;
    [SerializeField] private PreBase _preBasePrefab;
    [SerializeField] private Buyer _buyer;
    [SerializeField] private int _initialNumberOfMinersAtFirstBase;

    private readonly float _zeroPositionY = 0.15f;

    private void Start()
    {
        CreateBase(Vector3.zero, _initialNumberOfMinersAtFirstBase, true);
    }

    public Base CreateBase(Vector3 position, int initialMinersCount = 0, bool isActive = false)
    {
        float offsetY = -0.63f;

        if (initialMinersCount < 0)
            throw new ArgumentOutOfRangeException(nameof(initialMinersCount));

        if (initialMinersCount > 0)
            offsetY = _zeroPositionY;

        position.y = offsetY;
        Base baseClone = Instantiate(_basePrefab, position, transform.rotation);

        baseClone.transform.SetParent(transform);
        baseClone.InitializeData(_minerPrefab, _camera, _buyer, initialMinersCount);

        if (isActive)
            baseClone.Enable();

        return baseClone;
    }

    public void BuildBase(Base activeBase, Vector3 newBasePosition)
    {
        PreBase preBase = Instantiate(_preBasePrefab, newBasePosition, transform.rotation);

        activeBase.CreateColonist(preBase, CreateBase(newBasePosition));
    }
}
