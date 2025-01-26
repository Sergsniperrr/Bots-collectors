using System;
using UnityEngine;

public class ToolsView : MonoBehaviour
{
    private Stick _stick;
    private Hammer _hummer;
    private HammerStick _hammerStick;

    private void Awake()
    {
        _stick = GetComponentInChildren<Stick>();
        _hummer = GetComponentInChildren<Hammer>();
        _hammerStick = GetComponentInChildren<HammerStick>();

        if (_stick == null)
            throw new NullReferenceException(nameof(_stick));

        if (_hummer == null)
            throw new NullReferenceException(nameof(_hummer));

        if (_hammerStick == null)
            throw new NullReferenceException(nameof(_hammerStick));
    }

    public void HideInstruments()
    {
        _stick.Disable();
        _hummer.Disable();
        _hammerStick.Disable();
    }

    public void ShowInstruments()
    {
        _stick.Enable();
        _hummer.Enable();
        _hammerStick.Enable();
    }
}
