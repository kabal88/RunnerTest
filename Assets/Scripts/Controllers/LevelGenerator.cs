using System.Linq;
using Models;
using Services;
using Systems;
using UnityEngine;
using Views;

namespace Controllers
{
    public class LevelGenerator
    {
        private LevelGeneratorModel _model;
        private LevelHolder _holder;

        public LevelGenerator(LevelGeneratorModel model, GameObject prefab)
        {
            _holder = MonoBehaviour.Instantiate(prefab).GetComponent<LevelHolder>();
            _model = model;
        }

        public void GenerateLevel(int index)
        {
            CleanLevel();

            var config = _model.GetLevelConfig(index);
            GenerateRoad(config);
        }


        private void CleanLevel()
        {
            foreach (var r in _model.CurrentSegments.ToArray())
                MonoBehaviour.Destroy(r);
            
            _model.CurrentSegments.Clear();
        }

        private void GenerateRoad(RoadConfig config)
        {
            var spawnPoint = ServiceLocator.Get<SpawnService>()
                .GetObjectsByPredicate(x => x.Data.Id == SpawnPointIdentifierMap.RoadSpawnPoint).First().Data;

            var segments = config.Segments;
            float roadLength = 0;

            for (int i = 0; i < segments.Length; i++)
            {
                var pos = new Vector3(0, 0, (roadLength));
                pos += spawnPoint.Position;
                
                var segment = MonoBehaviour.Instantiate(segments[i].Prefab, pos, spawnPoint.Rotation);
                segment.transform.SetParent(_holder.transform);
                _model.CurrentSegments.Add(segment);
                
                roadLength += segments[i].SegmentLenght;
            }
        }
    }
}