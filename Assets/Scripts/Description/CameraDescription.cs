using System;
using Controllers;
using Identifier;
using Interfaces;
using UnityEngine;

namespace Description
{
    [Serializable]
    public class CameraDescription : ICameraDescription
    {
        [SerializeField] private CameraIdentifier _id;
        [SerializeField] private float _speed;

        public int Id => _id.Id;
        public CameraModel Model => new(_speed);
    }
}