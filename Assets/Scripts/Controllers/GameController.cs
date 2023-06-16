using Interfaces;
using Libraries;
using Models;
using Services;
using System;
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


        public bool IsAlive { get; }

        public GameController(GameModel gameModel,
            Library library,
            GameUIView gameUi,
            CameraView cameraView)
        {
            _model = gameModel;
            _library = library;
            _gameUIController = new GameUIController(gameUi);
            _cameraController = new CameraController(cameraView);
            _playerModel = new PlayerModel();
        }

        public void Init()
        {
            InitServices();
            
            _levelGenerator = new LevelGenerator(_library.GetLevelGeneratorDescription(_model.LevelGeneratorId).Model);

            Start();
        }

        private void Start()
        {
            _levelGenerator.GenerateLevel(_playerModel.Level);
            _updateLocalService.RegisterObject(_cameraController);
            _inputListenerService.RegisterObject(_cameraController);
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