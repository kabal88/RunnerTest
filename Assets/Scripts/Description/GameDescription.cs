using System;
using System.Collections.Generic;
using Data;
using Identifier;
using Interfaces;
using Models;
using UnityEngine;
using UnityEngine.Serialization;

namespace Descriptions
{
    [Serializable]
    public class GameDescription : IGameDescription
    {
        [SerializeField] private GameIdentifier _id;
        [SerializeField] private LevelGeneratorIdentifier _levelGeneratorId;

        public int Id => _id.Id;
        public GameModel Model => new GameModel(_levelGeneratorId.Id);
    }
}