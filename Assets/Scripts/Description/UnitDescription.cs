﻿using System;
using Identifier;
using Interfaces;
using Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Descriptions
{
    [Serializable]
    public class UnitDescription : IUnitDescription
    {
        [SerializeField] private UnitIdentifier _id;
        [SerializeField] private int _startNumber;
        [SerializeField] private float _speed;
        [SerializeField] private float _leftBorder;
        [SerializeField] private float _rightBorder;
        [SerializeField, AssetsOnly] private GameObject _prefab;


        public int Id => _id.Id;
        public GameObject Prefab => _prefab;


        public UnitModel Model => new(
            _startNumber,
            _leftBorder,
            _rightBorder,
            _speed);
    }
}