using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OreCountView : MonoBehaviour
{
    private readonly string _title = "  ÑÊËÀÄ\n ————\n";

    private IOreCounter _counter;
    private Text _text;

    private void Awake()
    {
        _counter = transform.parent.GetComponentInChildren<IOreCounter>();
        _text = GetComponentInChildren<Text>();

        if (_counter == null)
            throw new MissingComponentException(nameof(IOreCounter));

        if (_text == null)
            throw new MissingComponentException(nameof(Text));

        _text.text = $"{_title}   ïóñòî";
    }

    private void OnEnable()
    {
        _counter.OreAdded += ChangeText;
    }

    private void OnDisable()
    {
        _counter.OreAdded -= ChangeText;
    }

    private void ChangeText(Dictionary<string, int> counter)
    {
        string text = _title;

        foreach (string ore in counter.Keys)
            text += $"{ore} : {counter[ore]}\n";

        _text.text = text;
    }
}
