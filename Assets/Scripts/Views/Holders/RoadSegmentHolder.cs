using System;
using System.Collections.Generic;
using Data;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Views
{
    public class RoadSegmentHolder : MonoBehaviour
    {
        [SerializeField] private SimpleNumber[] _colorables;
        
        public IEnumerable<IColorableNumber> ColorableNumbers => _colorables;

        private void Awake()
        {
            if (_colorables == null)
            {
                _colorables = GetComponentsInChildren<SimpleNumber>();
            }
        }

        public void SetColorByPredicate(NumberColor colorData, Func<IColorableNumber, bool> predicate)
        {
            foreach (var colorable in _colorables)
            {
                if (predicate(colorable))
                {
                    colorable.SetColor(colorData);
                }
            }
        }

#if UNITY_EDITOR
        [Button(SdfIconType.Search, IconAlignment.LeftOfText)]
        public void CollectColorableNumbers()
        {
            _colorables = GetComponentsInChildren<SimpleNumber>();
        }
#endif
    }
}