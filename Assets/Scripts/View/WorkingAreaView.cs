using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class WorkingAreaView : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Show() => _renderer.enabled = true;
    public void Hide() => _renderer.enabled = false;
}
