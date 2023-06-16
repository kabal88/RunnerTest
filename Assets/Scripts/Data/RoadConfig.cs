using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

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
    public float Order;
   [AssetsOnly] public GameObject Prefab;
    public float SegmentLenght;
}
