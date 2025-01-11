using System;
using UnityEngine;

[RequireComponent(typeof(OreDetector))]
public class Base : MonoBehaviour, IContainer, IObservable
{
    [SerializeField] private int _initialMinersCount = 3;

    private MinersHandler _minersHandler;
    private Store _store;
    private OreDetector _detector;

    public event Action<Transform> CameraInitialized;

    public Vector3 Position => transform.position;

    private void Awake()
    {
        _detector = GetComponent<OreDetector>();

        _minersHandler = transform.GetComponentInChildren<MinersHandler>();
        _store = transform.GetComponentInChildren<Store>();

        if (_minersHandler == null)
            throw new NullReferenceException(nameof(_minersHandler));

        if (_store == null)
            throw new NullReferenceException(nameof(_store));
    }

    private void Start()
    {
        for (int i = 0; i < _initialMinersCount; i++)
            _minersHandler.CreateMiner();
    }

    private void OnEnable()
    {
        _detector.OreDetected += _minersHandler.CollectOre;
    }

    private void OnDisable()
    {
        _detector.OreDetected -= _minersHandler.CollectOre;
    }

    public void AddToStore(Ore ore) => _store.Add(ore);

    public void InitializeData(Miner minerPrefab, Transform camera)
    {
        if (minerPrefab == null)
            throw new ArgumentNullException(nameof(minerPrefab));

        if (camera == null)
            throw new ArgumentNullException(nameof(camera));

        _minersHandler.InitializeData(minerPrefab, this);
        CameraInitialized.Invoke(camera);
    }
}
