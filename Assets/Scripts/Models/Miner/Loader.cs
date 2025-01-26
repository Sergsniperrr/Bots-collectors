using System;
using System.Collections;
using UnityEngine;

public class Loader : MonoBehaviour
{
    private Ore _ore;
    private LeftHand _hand;
    private IContainer _store;

    public event Action ActionCompleted;

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
        StartCoroutine(LoadingOre(ore));
    }

    public void UnloadOre()
    {
        StartCoroutine(UnloadingOre());
    }

    public void FinishUnload() // »—œŒÀ‹«”≈“—ﬂ ¿Õ»Ã¿“Œ–ŒÃ!
    {
        _store.AddToStore(_ore);

        ActionCompleted?.Invoke();
    }

    public void LoadOre(Ore ore) // »—œŒÀ‹«”≈“—ﬂ ¿Õ»Ã¿“Œ–ŒÃ!
    {
        Vector3 position = new (-0.211f, -0.268f, 0.139f);
        Vector3 rotation = new (330.3f, 0f, 0f);

        _ore = ore != null ? ore : throw new ArgumentNullException(nameof(ore));

        _ore.transform.SetParent(_hand.transform);
        _ore.transform.localPosition = position;
        _ore.transform.localRotation = Quaternion.Euler(rotation);
    }

    private IEnumerator UnloadingOre()
    {
        float duration = 0.7f;
        var wait = new WaitForSeconds(duration);

        yield return wait;

        FinishUnload();
    }

    private IEnumerator LoadingOre(Ore ore)
    {
        float duration = 0.6f;
        var wait = new WaitForSeconds(duration);

        yield return wait;

        LoadOre(ore);

        StartCoroutine(FinishingLoad());
    }

    private IEnumerator FinishingLoad()
    {
        float duration = 0.2f;
        var wait = new WaitForSeconds(duration);

        yield return wait;

        ActionCompleted?.Invoke();
    }
}
