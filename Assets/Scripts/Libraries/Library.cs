using System;
using System.Collections.Generic;
using System.Linq;
using Descriptions;
using Helpers;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Libraries
{
    [Serializable]
    public sealed class Library  //TODO: make it auto generatable
    {
        [SerializeField] private List<Description> Descriptions = new();
        
        private Dictionary<int, IGameDescription> _gameDescriptions = new();
        private Dictionary<int, IUnitDescription> _unitCharacteristicsDescriptions = new();
        private Dictionary<int, ILevelGeneratorDescription> _levelGeneratorDescriptions = new();


        public void Init()
        {
            foreach (var description in Descriptions)
            {
                switch (description.GetDescription)
                {
                    case IGameDescription data:
                        _gameDescriptions.Add(description.GetDescription.Id, data);
                        break;
                    case IUnitDescription data:
                        _unitCharacteristicsDescriptions.Add(description.GetDescription.Id,data);
                        break;
                    case ILevelGeneratorDescription data:
                        _levelGeneratorDescriptions.Add(description.GetDescription.Id,data);
                        break;
                }
            }
        }
        
#if UNITY_EDITOR
        
        /// <summary>
        /// Work ONLY from Editor. Use after adding new Description to project. 
        /// </summary>
        [Button(ButtonSizes.Large), GUIColor(0.5f, 0.8f, 1f), PropertyTooltip("Click after adding new Description to project.")]
        public void CollectAllDescriptions()
        {
            Descriptions = new SOProvider<Description>().GetCollection().ToList();
        }
        
#endif
        public IGameDescription GetGameDescription(int id)
        {
            if (_gameDescriptions.TryGetValue(id, out var needed))
            {
                return needed;
            }
            throw new Exception($"Game description with id {id} not found");
        }

        public IUnitDescription GetUnitDescription(int id)
        {
            if (_unitCharacteristicsDescriptions.TryGetValue(id, out var needed))
            {
                return needed;
            }
            throw new Exception($"Unit description with id {id} not found");
        }
        
        public ILevelGeneratorDescription GetLevelGeneratorDescription(int id)
        {
            if (_levelGeneratorDescriptions.TryGetValue(id, out var needed))
            {
                return needed;
            }
            throw new Exception($"LevelGenerator description with id {id} not found");
        }
    }
}