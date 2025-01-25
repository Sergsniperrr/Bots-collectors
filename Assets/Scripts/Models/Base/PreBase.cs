using UnityEngine;

public class PreBase : MonoBehaviour
{
    private ParticleSystem _dust;

    private void Awake()
    {
        _dust = GetComponentInChildren<ParticleSystem>();
    }

    public void EnableDust() => _dust.Play();
}
