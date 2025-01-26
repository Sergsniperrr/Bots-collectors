using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Instrument : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Enable() => _renderer.enabled = true;
    public void Disable() => _renderer.enabled = false;
}
