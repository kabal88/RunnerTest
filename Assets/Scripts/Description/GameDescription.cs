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
        [SerializeField] private InputDescriptionIdentifier _inputDescriptionId;
        [SerializeField] private ColorsPalletIdentifier _colorPalletId;

        public int Id => _id.Id;
        public GameModel Model => new(_levelGeneratorId.Id, _colorPalletId.Id, _inputDescriptionId.Id);
    }
}