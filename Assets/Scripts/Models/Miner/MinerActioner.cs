using System;
using System.Collections;
using UnityEngine;

public class MinerActioner : MonoBehaviour
{
    private Ore _ore;
    private LeftHand _hand;
    private IContainer _store;
    private float _loadSpeed = 2f;
    private float _unloadSpeed = 1.3f;

    public event Action BuildCompleted;

    private void Awake()
    {
        _hand = GetComponentInChildren<LeftHand>();

        if (_hand == null)
            throw new NullReferenceException(nameof(_hand));
    }

    public void SetStore(IContainer store)
    {
        _store = store ?? throw new ArgumentNullException(nameof(store));
    }

    public void PickUp(Ore ore)
    {
        _ore = ore != null ? ore : throw new ArgumentNullException(nameof(ore));

        _ore.transform.SetParent(transform);
    }

    public void FinishUnload() // »—œŒÀ‹«”≈“—ﬂ ¿Õ»Ã¿“Œ–ŒÃ!
    {
        _store.AddToStore(_ore);
    }

    private IEnumerator MoveOre(Vector3 targetPosition, float speed)
    {
        while (_ore.transform.localPosition != targetPosition)
        {
            _ore.transform.localPosition = Vector3.MoveTowards(_ore.transform.localPosition, targetPosition, Time.deltaTime * speed);
            yield return null;
        }
    }

    public void LoadOre() // »—œŒÀ‹«”≈“—ﬂ ¿Õ»Ã¿“Œ–ŒÃ!
    {
        Vector3 targetPosition = new(-0.014f, 1.065f, 0.326f);

        StartCoroutine(MoveOre(targetPosition, _loadSpeed));
    }

    public void UnloadOre()
    {
        Vector3 targetPosition = new(0f, 0.22f, 0.5f);

        StartCoroutine(MoveOre(targetPosition, _unloadSpeed));
    }

    public void Build()
    {
        float duration = 10f;

        StartCoroutine(BuildInTime(duration));
    }

    private IEnumerator BuildInTime(float duration)
    {
        var wait = new WaitForSeconds(duration);

        yield return wait;

        BuildCompleted?.Invoke();
    }
}
