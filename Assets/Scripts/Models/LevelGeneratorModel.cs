using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public class LevelGeneratorModel
    {
        public List<GameObject> CurrentSegments = new();
        
        private int _numberOfLevelsForRepeating = 3;
        private RoadConfig[] _roadConfigs;
        private Dictionary<int, RoadConfig> configs ;


        public LevelGeneratorModel(int numberOfLevelsForRepeating, RoadConfig[] roadConfigs)
        {
            _numberOfLevelsForRepeating = numberOfLevelsForRepeating;
            _roadConfigs = roadConfigs;
            
            configs = new ();
            
            for (int i = 0; i < roadConfigs.Length; i++)
            {
                RoadConfig c = roadConfigs[i];
                configs.Add(i, c);
            }
        }

        public RoadConfig GetLevelConfig(int index)
        {
            if (configs.TryGetValue(index, out var roadConfig))
            {
                return roadConfig;
            }

            roadConfig = GetResolvedConfig(index);
            return roadConfig;
        }
        
        private RoadConfig GetResolvedConfig(int index)
        {
            var resolvedIndex = _roadConfigs.Length - _numberOfLevelsForRepeating + (index - _roadConfigs.Length) % _numberOfLevelsForRepeating;

            if (resolvedIndex < 0)
            {
                resolvedIndex = 0;
            }

            return _roadConfigs[resolvedIndex];
        }
    }
}