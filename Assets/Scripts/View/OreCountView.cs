using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OreCountView : MonoBehaviour
{
    private readonly string _title = "  ÑÊËÀÄ\n ————\n";

    private IOreCounter _counter;
    private Text _text;
    private string _empty;

    private void Awake()
    {
        _counter = transform.parent.GetComponentInChildren<IOreCounter>();
        _text = GetComponentInChildren<Text>();

        if (_counter == null)
            throw new MissingComponentException(nameof(IOreCounter));

        if (_text == null)
            throw new MissingComponentException(nameof(Text));

        _empty = $"{_title}   ïóñòî";

        _text.text = _empty;
    }

    private void OnEnable()
    {
        _counter.OreCountChanged += ChangeText;
    }

    private void OnDisable()
    {
        _counter.OreCountChanged -= ChangeText;
    }

    private void ChangeText(Dictionary<string, int> counter)
    {
        string text = _title;

        if (counter.Count == 0)
            _text.text = _empty;
        else
            _text.text = _title;

        foreach (KeyValuePair<string, int> ore in counter)
            text += $"{ore.Key} : {ore.Value}\n";

        _text.text = text;
    }
}
