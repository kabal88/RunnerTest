using Interfaces;
using Libraries;
using Models;
using Services;
using System;
using System.Linq;
using Systems;
using Views;


namespace Controllers
{
    public class GameController : IUpdatable, IFixUpdatable, IDisposable
    {
        private GameModel _model;
        private PlayerModel _playerModel;
        private Library _library;
        private UpdateLocalService _updateLocalService;
        private FixUpdateLocalService _fixUpdateLocalService;
        private SpawnService _spawnService;
        private GameUIController _gameUIController;
        private LevelGenerator _levelGenerator;
        private InputListenerService _inputListenerService;
        private CameraController _cameraController;
        private UnitController _unitController;


        public bool IsAlive { get; }

        public GameController(GameModel gameModel,
            Library library,
            GameUIView gameUi,
            CameraView cameraView)
        {
            IsAlive = true;
            _model = gameModel;
            _library = library;
            _gameUIController = new GameUIController(gameUi);
            _cameraController = new CameraController(cameraView,
                _library.GetCameraDescription(_model.CameraDescriptionId).Model);
            _playerModel = new PlayerModel();
        }

        public void Init()
        {
            InitServices();

            var levelGeneratorDescription = _library.GetLevelGeneratorDescription(_model.LevelGeneratorId);
            _levelGenerator = new LevelGenerator(levelGeneratorDescription.Model, levelGeneratorDescription.Prefab);
            _levelGenerator.GenerateLevel(_playerModel.Level);

            _unitController = CreateUnit();
            _cameraController.Init();

            _updateLocalService.RegisterObject(_cameraController);
            _inputListenerService.RegisterObject(_cameraController);

            Start();
        }

        private void Start()
        {
            _unitController.HandleState(_unitController.MovingState);
        }

        private UnitController CreateUnit()
        {
            var description = _library.GetUnitDescription(_model.UnitDescriptionId);
            var spawnPoint = _spawnService.GetObjectsByPredicate(
                x => x.Data.Id == SpawnPointIdentifierMap.UnitSpawnPoint).First();

            return new UnitController(description.Model, description.Prefab, spawnPoint.Parent, spawnPoint.Data);
        }


        private void Restart()
        {
            _levelGenerator.GenerateLevel(_playerModel.Level);
        }

        public void UpdateLocal(float deltaTime)
        {
            _updateLocalService.UpdateLocal(deltaTime);
        }

        public void FixedUpdateLocal()
        {
            _fixUpdateLocalService.FixedUpdateLocal();
        }

        private void InitServices()
        {
            _updateLocalService = new UpdateLocalService();
            ServiceLocator.SetService(_updateLocalService);
            _fixUpdateLocalService = new FixUpdateLocalService();
            ServiceLocator.SetService(_fixUpdateLocalService);
            _spawnService = new SpawnService();
            ServiceLocator.SetService(_spawnService);
            _spawnService.Init();
            _inputListenerService =
                new InputListenerService(_library.GetInputDescription(_model.InputDescriptionId).Model);
            ServiceLocator.SetService(_inputListenerService);
        }

        public void Dispose()
        {
        }
    }
}