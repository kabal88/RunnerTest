using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUi : MonoBehaviour
{
    private LevelWidget _levelWidget;

    public void Init()
    {
        _levelWidget = GetComponentInChildren<LevelWidget>();
    }

    public void SetLevel(int value)
    {
        _levelWidget.SetText($"Level {value}");
    }
}