using System;
using Identifier;
using Interfaces;
using UnityEngine;

namespace Descriptions
{
    [Serializable]
    public class UnitDescription : IUnitDescription
    {
        [SerializeField] private UnitIdentifier _id;
        [SerializeField] private float _speed;

        public int Id => _id.Id;
        public float Speed => _speed;
    }
}