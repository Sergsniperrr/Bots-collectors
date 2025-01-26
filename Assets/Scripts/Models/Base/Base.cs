using System;
using UnityEngine;

[RequireComponent(typeof(OreDetector))]
public class Base : MonoBehaviour, IContainer, IObservable, IColonizable
{
    [field: SerializeField] public float DurationOfBuild { get; private set; }

    private MinersHandler _minersHandler;
    private Store _store;
    private OreDetector _detector;
    private WorkingAreaView _workingArea;
    private Canvas _textCanvas;
    private Vector3 _position = Vector3.zero;

    public event Action<Transform> CameraInitialized;

    public Vector3 Position => transform.position;

    private void Awake()
    {
        _detector = GetComponent<OreDetector>();

        _minersHandler = transform.GetComponentInChildren<MinersHandler>();
        _store = transform.GetComponentInChildren<Store>();
        _workingArea = transform.GetComponentInChildren<WorkingAreaView>();
        _textCanvas = transform.GetComponentInChildren<Canvas>();

        if (_minersHandler == null)
            throw new NullReferenceException(nameof(_minersHandler));

        if (_store == null)
            throw new NullReferenceException(nameof(_store));

        if (_workingArea == null)
            throw new NullReferenceException(nameof(_workingArea));

        if (_textCanvas == null)
            throw new NullReferenceException(nameof(_textCanvas));
    }

    private void OnEnable()
    {
        _detector.OreDetected += _minersHandler.CollectOre;
    }

    private void OnDisable()
    {
        _detector.OreDetected -= _minersHandler.CollectOre;
    }

    public void InitializeData(Miner minerPrefab, Transform camera, int initialMinersCount = 0)
    {
        if (minerPrefab == null)
            throw new ArgumentNullException(nameof(minerPrefab));

        if (camera == null)
            throw new ArgumentNullException(nameof(camera));

        if (initialMinersCount < 0)
            throw new ArgumentOutOfRangeException(nameof(initialMinersCount));

        _minersHandler.InitializeData(minerPrefab, this);
        CameraInitialized.Invoke(camera);

        for (int i = 0; i < initialMinersCount; i++)
            _minersHandler.CreateMiner();
    }

    public void AddToStore(Ore ore) => _store.Add(ore);
    public void ShowArea() => _workingArea.Show();
    public void HideArea() => _workingArea.Hide();
    public void AddMiner(Miner miner) => _minersHandler.AddMiner(miner);

    public void Enable()
    {
        _textCanvas.enabled = true;
        _detector.StartScan();
    }

    public void CreateColonist(PreBase preBase, IColonizable newBase)
    {
        _minersHandler.CreateColonist(preBase, newBase);
    }

    public void SetPositionY(float value)
    {
        _position = transform.position;
        _position.y = value;
        transform.position = _position;
    }
}
