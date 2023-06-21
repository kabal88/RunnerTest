using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "RoadConfig", menuName = "BluePrints/RoadConfig")]
public class RoadConfig : ScriptableObject
{
    [SerializeField] private float _order;
    [SerializeField, TableList] private SegmentConfig[] _segments;

    public float Order => _order;
    public SegmentConfig[] Segments => _segments;


#if UNITY_EDITOR
    [Button(SdfIconType.Stack, IconAlignment.LeftOfText)]
    public void SortByOrder()
    {
        if (_segments == null)
            return;

        var sorted = _segments.OrderBy(x => x.Order);
        _segments = sorted.ToArray();
    }
#endif
}

[Serializable]
public struct SegmentConfig
{
    [TableColumnWidth(50, Resizable = false)] public float Order;
    [AssetsOnly] public GameObject Prefab;
    [TableColumnWidth(60, Resizable = false)] public float Lenght;
}